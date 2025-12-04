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
        // Inscreve os métodos nos eventos do stateManager
        stateManager.OnStateChanged += OnStateChanged;
        stateManager.OnCashoutValueChanged += OnCashoutValueChanged;

        UpdateButtonBehavior(stateManager.CurrentState);
    }

    void OnDestroy()
    {
        // Remove os eventos para evitar erros quando o GameObject for destruído
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

        if (state == BetButtonState.ReadyToBet)
        {
            betButton.onClick.AddListener(HandleBetClick);
        }
        else if (state == BetButtonState.CanCashout)
        {
            betButton.onClick.AddListener(HandleCashoutClick);
        }
        // WaitingCashout: botão desabilitado, nenhum listener
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
    }

    public void UpdateCashoutValue()
    {
        stateManager.UpdateCashoutValue();
    }
}