using TMPro;
using UnityEngine;

public class Bombselector : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI dropdown;

    //Escolher a quantidade de minas
    public void SetBombAmount()
    {
        if (int.TryParse(dropdown.text, out int bombCount))
        {
            gameManager.bombAmount = bombCount;
        }
    }
}
