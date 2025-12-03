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
    
    // é chamado quando o objeto é destruído e limpa inscrições de eventos.
    // Isso evita erros e referências quando o GameObject deixar de existir.
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
    
    // atualiza a aparência e a função do botão quando o estado muda.
    // sincroniza a UI e o comportamento conforme o novo estado recebido.
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
        // se o jogador puder apostar toca o som
        if (betPlaced)
        {
            audioManager.BetClick();
        }
        // se não puder mostra o popup de saldo insuficiente
        else
        {
            uiController.ShowInsufficientBalanceMessage();
        }
    }
    
    // toca o som, executa o cashout e chama o método do estado.
    // Depois disso, inicia a sequência que restaura o botão ao estado inicial.
    private void HandleCashoutClick()
    {
        audioManager.CashoutSound();
        stateManager.ExecuteCashout();
        uiController.StartRestartSequence();
    }
    
    // repassa a solicitação para atualizar o valor de cashout ao stateManager.
    // Ele serve apenas para encaminhar, para manter a lógica organizada.
    // Método público caso precise atualizar o valor do cashout no unity
    public void UpdateCashoutValue()
    {
        stateManager.UpdateCashoutValue();
    }
}