using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBet : MonoBehaviour
{
    public Button betButton;
    [SerializeField] private GameManager _gameManager;

    public TextMeshProUGUI buttonBetText;
  

    public Image buttonBetLaranja;
    public Image buttonBetVermelho;
    private Image buttonBetAtual;

    void Start()
    {
        buttonBetAtual = buttonBetLaranja;

        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        // limpa listeners antigos
        betButton.onClick.RemoveAllListeners();

        if (!_gameManager.active)
        {
            betButton.onClick.AddListener(IsGaming);
        }
        
        //else if (_gameManager && mineActive)
        //{
        //    betButton.onClick.AddListener(PossibleCashout);
        //}
        else if (_gameManager.active)
        {
            //cashout = true;
            betButton.onClick.AddListener(PossibleCashout);
        }
    }

    void IsGaming()
    {
        buttonBetText.text = "CASHOUT\n4,00 BRL";
        SetButtonAlpha(betButton, 0.2f);
        buttonBetText.fontSize = 25;
        betButton.image.sprite = buttonBetVermelho.sprite;
        UpdateButtonState();
    }


    void PossibleCashout()
    {
        buttonBetText.text = "BET";
        
        betButton.image.sprite = buttonBetLaranja.sprite;

        UpdateButtonState();
    }

    void SetButtonAlpha(Button button, float alpha)
    {
        Color c = button.image.color;
        c.a = alpha;
        button.image.color = c;
    }

    


}
