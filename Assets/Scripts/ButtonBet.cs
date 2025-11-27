using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBet : MonoBehaviour
{
    public Button betButton;
    public Button mineButton;

    public TextMeshProUGUI buttonBetText;
    private string betText = "BET";
    private bool onGaming = false;
    private bool cashout = false;
    private bool mineActive = false;

    public Image buttonBetLaranja;
    public Image buttonBetVermelho;
    private Image buttonBetAtual;

    public Image buttonMineRosa;
    public Image buttonMineAmarelo;
    private Image buttonMineAtual;

    void Start()
    {
        buttonBetAtual = buttonBetLaranja;
        buttonMineAtual = buttonMineRosa;
        
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        // limpa listeners antigos
        betButton.onClick.RemoveAllListeners();
        mineButton.onClick.RemoveAllListeners();

        if (!onGaming)
        {
            betButton.onClick.AddListener(IsGaming);
        }
        else if (onGaming && !mineActive)
        {
            mineButton.onClick.AddListener(MineTeste);
        }
        //else if (onGaming && mineActive)
        //{
        //    betButton.onClick.AddListener(PossibleCashout);
        //}
        else if (onGaming && mineActive)
        {
            //cashout = true;
            betButton.onClick.AddListener(PossibleCashout);
        }
    }

    void IsGaming()
    {
        buttonBetText.text = "CASHOUT\n4,00 BRL";
        onGaming = true;
        SetButtonAlpha(betButton, 0.5f);
        betButton.image.sprite = buttonBetVermelho.sprite;

        UpdateButtonState();
    }


    void PossibleCashout()
    {
        buttonBetText.text = "BET";
        onGaming = false;
        mineActive = false;
        cashout = false;
        betButton.image.sprite = buttonBetLaranja.sprite;
        mineButton.image.sprite = buttonMineRosa.sprite;

        UpdateButtonState();
    }

    void SetButtonAlpha(Button button, float alpha)
    {
        Color c = button.image.color;
        c.a = alpha;
        button.image.color = c;
    }

    void MineTeste()
    {
        cashout = true;
        SetButtonAlpha(betButton, 1.0f);

        mineActive = true;
        mineButton.image.sprite = buttonMineAmarelo.sprite;
        UpdateButtonState();
    }


}
