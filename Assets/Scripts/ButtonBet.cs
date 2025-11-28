using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBet : MonoBehaviour
{
    public Button betButton;
    public bool cashout;
    [SerializeField] private GameManager _gameManager;

    public TextMeshProUGUI buttonBetText;
  

    public Image buttonBetLaranja;
    public Image buttonBetVermelho;
    private Image buttonBetAtual;

    void Start()
    {
        buttonBetAtual = buttonBetLaranja;
        cashout = false;

        UpdateButtonState();
    }

    void Update()
    {
        //if (!_gameManager.active)
        //{
        //    RestartGame();
        //}
        if (_gameManager.active && (_gameManager.gameFase > 0) && !cashout)
        {
            PossibleCashout();
        }
    }

    void UpdateButtonState()
    {
        // limpa listeners antigos
        betButton.onClick.RemoveAllListeners();

        if (!_gameManager.active)
        {
            betButton.onClick.AddListener(IsGaming);
        }

        else if (_gameManager.active && (_gameManager.gameFase > 0) && cashout)
        {
            betButton.onClick.AddListener(RestartButtonBet);
        }

    }

    void IsGaming()
    {
        buttonBetText.text = "CASHOUT\n4,00 BRL";
        SetButtonAlpha(betButton, 0.7f);
        buttonBetText.fontSize = 25;
        betButton.image.sprite = buttonBetVermelho.sprite;
        UpdateButtonState();
    }


    void PossibleCashout()
    {
        cashout = true;
        SetButtonAlpha(betButton, 1.0f);       
        UpdateButtonState();
    }

    public void RestartButtonBet()
    {
        buttonBetText.text = "BET";
        buttonBetText.fontSize = 40;
        //SetButtonAlpha(betButton, 1.0f);
        betButton.image.sprite = buttonBetLaranja.sprite;
        cashout = false;
        UpdateButtonState();
    }

    void SetButtonAlpha(Button button, float alpha)
    {
        Color c = button.image.color;
        c.a = alpha;
        button.image.color = c;
    }

    


}
