using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BetUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button betButton;
    [SerializeField] private TextMeshProUGUI buttonBetText;
    [SerializeField] private TextMeshProUGUI insufficientBalanceText;
    
    [Header("Visual Assets")]
    [SerializeField] private Sprite buttonBetLaranja;
    [SerializeField] private Sprite buttonBetVermelho;
    
    [Header("Configuration")]
    private const float DISABLED_ALPHA = 0.5f;
    private const float ENABLED_ALPHA = 1.0f;
    private const float RESTART_DELAY = 3f;
    private const float INSUFFICIENT_BALANCE_DELAY = 2f;
    private const int BET_FONT_SIZE = 40;
    private const int CASHOUT_FONT_SIZE = 25;
    
    private Image buttonImage;
    private Coroutine restartCoroutine;
    private Coroutine insufficientBalanceCoroutine;
    
    void Awake()
    {
        buttonImage = betButton.image;
    }
    
    public void ApplyVisualState(BetButtonState state)
    {
        switch (state)
        {
            case BetButtonState.ReadyToBet:
                ShowReadyToBetState();
                break;
                
            case BetButtonState.WaitingCashout:
                ShowWaitingCashoutState();
                break;
                
            case BetButtonState.CanCashout:
                ShowCanCashoutState();
                break;
        }
    }
    
    private void ShowReadyToBetState()
    {
        SetButtonText("BET", BET_FONT_SIZE);
        SetButtonSprite(buttonBetLaranja);
        SetButtonAlpha(ENABLED_ALPHA);
        betButton.interactable = true;
    }
    
    private void ShowWaitingCashoutState()
    {
        SetButtonText("CASHOUT\n0.00 BRL", CASHOUT_FONT_SIZE);
        SetButtonSprite(buttonBetVermelho);
        SetButtonAlpha(DISABLED_ALPHA);
        betButton.interactable = false;
    }
    
    private void ShowCanCashoutState()
    {
        SetButtonSprite(buttonBetVermelho);
        SetButtonAlpha(ENABLED_ALPHA);
        betButton.interactable = true;
    }
    
    public void UpdateCashoutDisplay(float value)
    {
        SetButtonText($"CASHOUT\n{value:F2} BRL", CASHOUT_FONT_SIZE);
    }
    
    public void ShowInsufficientBalanceMessage()
    {
        if (insufficientBalanceCoroutine != null)
            StopCoroutine(insufficientBalanceCoroutine);
            
        insufficientBalanceCoroutine = StartCoroutine(InsufficientBalanceRoutine());
    }
    
    public void StartRestartSequence()
    {
        if (restartCoroutine != null)
            StopCoroutine(restartCoroutine);
            
        restartCoroutine = StartCoroutine(RestartButtonRoutine());
    }
    
    private void SetButtonText(string text, int fontSize)
    {
        buttonBetText.text = text;
        buttonBetText.fontSize = fontSize;
    }
    
    private void SetButtonSprite(Sprite sprite)
    {
        buttonImage.sprite = sprite;
    }
    
    private void SetButtonAlpha(float alpha)
    {
        Color color = buttonImage.color;
        color.a = alpha;
        buttonImage.color = color;
    }
    
    private IEnumerator RestartButtonRoutine()
    {
        yield return new WaitForSeconds(RESTART_DELAY);
        ShowReadyToBetState();
    }
    
    private IEnumerator InsufficientBalanceRoutine()
    {
        insufficientBalanceText.gameObject.SetActive(true);
        yield return new WaitForSeconds(INSUFFICIENT_BALANCE_DELAY);
        insufficientBalanceText.gameObject.SetActive(false);
    }
}