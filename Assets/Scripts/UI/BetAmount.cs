using TMPro;
using UnityEngine;

public class BetAmount : MonoBehaviour
{
    public int betAmount = 1;
    [SerializeField] private TextMeshProUGUI betAmountText;

    public void AddOneBetAmount()
    {
        if (betAmount < 500)
        {
            betAmount += 1;
            betAmountText.text = betAmount.ToString();
        }
        
    }
    public void MinusOneBetAmount()
    {
        if (betAmount >= 2)
        {
            betAmount -= 1;
            betAmountText.text = betAmount.ToString();
        }
        
    }
    public void DobleBetAmount()
    {
        betAmount *= 2;
        if (betAmount > 500)
        {
            betAmount = 500;
        }
        betAmountText.text = betAmount.ToString();
    }
    
    public void HalveBetAmount()
    {
        
        betAmount /= 2;
        if (betAmount < 1)
        {
            betAmount = 1;
        }
        betAmountText.text = betAmount.ToString();
    }
    
}
