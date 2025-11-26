using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class ButtonBet : MonoBehaviour
{
    public Button betButton;
    public TextMeshProUGUI buttonBetText;
    private string betText = "BET";
    private bool noGaming = true;
    private bool cashout = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //betButton.onClick.AddListener(OnBetButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        buttonBetText.text = betText;

        if (noGaming)
        {
            betButton.onClick.AddListener(IsGaming);
        } else if (!noGaming && cashout)
        {
            betButton.onClick.AddListener(EndGame);
        }
        
    }

    void IsGaming()
    {
        betText = "CASHOUT\n4,00 BRL";
        noGaming = false;
        cashout = true;
    }

    void EndGame()
    {
        betText = "BET";
        noGaming = true;
        cashout = false;
    }
}
