using UnityEngine;
using UnityEngine.UI;

public class ButtonBetTemp : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button betButton;
    [SerializeField] private BetStateManager stateManager;
    [SerializeField] private BetUIController uiController;
    [SerializeField] private AudioManager audioManager;
    
    void Start()
    {
        SetupEventListeners();
        UpdateButtonBehavior(stateManager.CurrentState);
    }
    
    void OnDestroy()
    {
        RemoveEventListeners();
    }
    
    private void SetupEventListeners()
    {
        stateManager.OnStateChanged += OnStateChanged;
        stateManager.OnCashoutValueChanged += OnCashoutValueChanged;
    }
    
    private void RemoveEventListeners()
    {
        stateManager.OnStateChanged -= OnStateChanged;
        stateManager.OnCashoutValueChanged -= OnCashoutValueChanged;
    }
    
    private void OnStateChanged(BetButtonState newState)
    {
        uiController.ApplyVisualState(newState);
        UpdateButtonBehavior(newState);
    }
    
    private void OnCashoutValueChanged(float value)
    {
        uiController.UpdateCashoutDisplay(value);
    }
    
    private void UpdateButtonBehavior(BetButtonState state)
    {
        betButton.onClick.RemoveAllListeners();
        
        switch (state)
        {
            case BetButtonState.ReadyToBet:
                betButton.onClick.AddListener(HandleBetClick);
                break;
                
            case BetButtonState.CanCashout:
                betButton.onClick.AddListener(HandleCashoutClick);
                break;
                
            case BetButtonState.WaitingCashout:
                // Botão desabilitado, sem listener necessário
                break;
        }
    }
    
    private void HandleBetClick()
    {
        bool betPlaced = stateManager.TryPlaceBet();
        
        if (betPlaced)
        {
            audioManager.BetClick();
        }
        else
        {
            uiController.ShowInsufficientBalanceMessage();
        }
    }
    
    private void HandleCashoutClick()
    {
        audioManager.CashoutSound();
        stateManager.ExecuteCashout();
        uiController.StartRestartSequence();
    }
    
    // Método público caso precise atualizar o valor do cashout externamente
    public void UpdateCashoutValue()
    {
        stateManager.UpdateCashoutValue();
    }
}