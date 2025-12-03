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
    private float disabledAlpha = 0.5f;
    private float enabledAlpha = 1.0f;
    private float restartDelay = 3f;
    private float insufficientBalanceDelay = 2f;
    private int betFontSize = 40;
    private int cashoutFontSize = 25;
    
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
                ReadyToBetState();
                break;
                
            case BetButtonState.WaitingCashout:
                WaitingCashoutState();
                break;
                
            case BetButtonState.CanCashout:
                CanCashoutState();
                break;
        }
    }
    
    private void ReadyToBetState()
    {
        SetButtonText("BET", betFontSize);
        SetButtonSprite(buttonBetLaranja);
        SetButtonAlpha(enabledAlpha);
        betButton.interactable = true;
    }
    
    private void WaitingCashoutState()
    {
        SetButtonText("CASHOUT\n0.00 BRL", cashoutFontSize);
        SetButtonSprite(buttonBetVermelho);
        SetButtonAlpha(disabledAlpha);
        betButton.interactable = false;
    }
    
    private void CanCashoutState()
    {
        SetButtonSprite(buttonBetVermelho);
        SetButtonAlpha(enabledAlpha);
        betButton.interactable = true;
    }
    
    public void UpdateCashoutDisplay(float value)
    {
        SetButtonText($"CASHOUT\n{value:F2} BRL", cashoutFontSize);
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
        yield return new WaitForSeconds(restartDelay);
        ReadyToBetState();
    }
    
    private IEnumerator InsufficientBalanceRoutine()
    {
        insufficientBalanceText.gameObject.SetActive(true);
        yield return new WaitForSeconds(insufficientBalanceDelay);
        insufficientBalanceText.gameObject.SetActive(false);
    }
}