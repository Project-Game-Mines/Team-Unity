using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBet : MonoBehaviour
{
    public Button betButton;
    public TextMeshProUGUI buttonBetText;
    private string betText = "BET";
    private bool onGaming = false;
    private bool cashout = false;

    public Image buttonBetLaranja;
    public Image buttonBetVermelho;

    private Image buttonBetAtual;

    void Start()
    {
        buttonBetAtual = buttonBetLaranja;
        UpdateButtonState();
    }

    void Update()
    {
        buttonBetText.text = betText;
        betButton.image.sprite = buttonBetAtual.sprite;
    }

    void UpdateButtonState()
    {
        // limpa listeners antigos
        betButton.onClick.RemoveAllListeners();

        if (!onGaming && !cashout)
        {
            betButton.onClick.AddListener(IsGaming);
        }
        else if (onGaming && cashout)
        {
            betButton.onClick.AddListener(EndGame);
        }
    }

    void IsGaming()
    {
        betText = "CASHOUT\n4,00 BRL";
        onGaming = true;
        cashout = true;
        buttonBetAtual = buttonBetVermelho;

        UpdateButtonState();
    }

    void EndGame()
    {
        betText = "BET";
        onGaming = false;
        cashout = false;
        buttonBetAtual = buttonBetLaranja;

        UpdateButtonState();
    }
}
