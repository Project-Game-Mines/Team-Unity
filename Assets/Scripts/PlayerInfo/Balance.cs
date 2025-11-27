using TMPro;
using UnityEngine;

public class Balance : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI balanceAmountText;
    [SerializeField] MockPlayer player;
    void Start()
    {
        balanceAmountText.text = $"R$ {player.balance.ToString()}";
    }

    
}
