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

        // jogo ainda não iniciou, botão está laranja e escrito "BET"
        if (!_gameManager.active)
        {
            Debug.Log("Caso 1");
            betButton.onClick.AddListener(IsGaming);
        }
        
        // jogo já começou, o jogador já pelo menos uma jogada, botão está vermelho e escrito "cashout"
        else if (_gameManager.active && (_gameManager.gameFase > 0))
        {
            Debug.Log("Caso 2");
            betButton.onClick.AddListener(RestartButtonBet);
        }

        // jogo começou mas o jogador ainda não fez nenhuma jogada, botão está vermelho claro e escrito "cashout"
        // (não faz nada)
        else
        {
            Debug.Log("Caso 3");
            betButton.onClick.AddListener(Nothing);
        }

    }

    void IsGaming()
    {
        _gameManager.StartGame();
        buttonBetText.text = "CASHOUT\n4,00 BRL";
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
    

}
