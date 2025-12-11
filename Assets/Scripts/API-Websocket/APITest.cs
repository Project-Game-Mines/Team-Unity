using UnityEngine;
using TMPro; // Necessário para TextMeshPro

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private APIManager apiManager; 
    
    [SerializeField] private TextMeshProUGUI balanceText;
    //Atulaliza o Texto do balance
    public void UpdateBalance()
    {
        balanceText.text = GameManager.player.balance.ToString("F2");
    }
}