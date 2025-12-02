using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI balanceAmountText;
    [SerializeField] MockPlayer player;
    
    void Update()
    {
        balanceAmountText.text = $"{player.balance.ToString("F2")}";
    }
    
    
}
