using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonBet : MonoBehaviour
{
    public Button betButton;
    [SerializeField] private GameManager _gameManager;

    public TextMeshProUGUI buttonBetText;
  

    public Image buttonBetLaranja;
    public Image buttonBetVermelho;

    void Start()
    {
        UpdateButtonState();
    }

    void Update()
    {
        
    }

    void UpdateButtonState()
    {
        // limpa listeners antigos
        betButton.onClick.RemoveAllListeners();

        // jogo ainda n�o iniciou, bot�o est� laranja e escrito "BET"
        if (!_gameManager.active)
        {
            Debug.Log("Caso 1");
            betButton.onClick.AddListener(IsGaming);
        }
        
        // jogo j� come�ou, o jogador j� pelo menos uma jogada, bot�o est� vermelho e escrito "cashout"
        else if (_gameManager.active && (_gameManager.gameFase > 0))
        {
            Debug.Log("Caso 2");
            betButton.onClick.AddListener(RestartButtonBet);
        }

        // jogo come�ou mas o jogador ainda n�o fez nenhuma jogada, bot�o est� vermelho claro e escrito "cashout"
        // (n�o faz nada)
        else
        {
            Debug.Log("Caso 3");
            betButton.onClick.AddListener(Nothing);
        }

    }

    void IsGaming()
    {
        _gameManager.StartGame();
        buttonBetText.text = $"CASHOUT\n{_gameManager.totalCheckout} BRL";
        betButton.image.sprite = buttonBetVermelho.sprite;
        SetButtonAlpha(betButton, 0.5f);
        buttonBetText.fontSize = 25;
        UpdateButtonState();
    }


    public void PossibleCashout()
    {
        SetButtonAlpha(betButton, 1.0f);
        UpdateButtonState();
    }

    void Nothing()
    {
        UpdateButtonState();
    }

    public void RestartButtonBet()
    {
        StartCoroutine(WaitSeconds());
    }

    void SetButtonAlpha(Button button, float alpha)
    {
        Color c = button.image.color;
        c.a = alpha;
        button.image.color = c;
    }

    private IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(3);
        _gameManager.GameOver();
        buttonBetText.text = "BET";
        buttonBetText.fontSize = 40;
        betButton.image.sprite = buttonBetLaranja.sprite;
        UpdateButtonState();
    }

    public void UpdateCheckOutPrice()
    {
        buttonBetText.text = $"CASHOUT\n{_gameManager.totalCheckout} BRL";
    }
    

}
