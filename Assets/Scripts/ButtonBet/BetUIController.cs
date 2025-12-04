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
    [SerializeField] private Sprite buttonBetOrange;
    [SerializeField] private Sprite buttonBetRed;
    
    private Image buttonImage;
    void Awake()
    {
        buttonImage = betButton.image;
        insufficientBalanceText.gameObject.SetActive(false);
    }

    // Atualiza a aparência do botão conforme o estado
    public void ApplyVisualState(BetButtonState state)
    {
        if (state == BetButtonState.ReadyToBet)
        {
            buttonBetText.text = "BET";
            buttonBetText.fontSize = 40;
            buttonImage.sprite = buttonBetOrange;
            SetButtonAlpha(1f);
            betButton.interactable = true;
        }
        else if (state == BetButtonState.WaitingCashout)
        {
            buttonBetText.text = "CASHOUT\n0.00 BRL";
            buttonBetText.fontSize = 25;
            buttonImage.sprite = buttonBetRed;
            SetButtonAlpha(0.5f);
            betButton.interactable = false;
        }
        else if (state == BetButtonState.CanCashout)
        {
            buttonImage.sprite = buttonBetRed;
            SetButtonAlpha(1f);
            betButton.interactable = true;
        }
    }

    // Atualiza o valor mostrado no botão quando o cashout muda
    public void UpdateCashoutDisplay(float value)
    {
        buttonBetText.text = $"CASHOUT\n{value:F2} BRL";
        buttonBetText.fontSize = 25;
    }

    // Mostra uma mensagem simples de saldo insuficiente
    public void ShowInsufficientBalanceMessage()
    {
        insufficientBalanceText.gameObject.SetActive(true);
        Invoke("HideInsufficientBalanceMessage", 2f);
    }

    private void HideInsufficientBalanceMessage()
    {
        insufficientBalanceText.gameObject.SetActive(false);
    }

    private void SetButtonAlpha(float alpha)
    {
        Color color = buttonImage.color;
        color.a = alpha;
        buttonImage.color = color;
    }
}