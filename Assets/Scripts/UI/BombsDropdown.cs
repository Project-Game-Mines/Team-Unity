using TMPro;
using UnityEngine;

public class BombsDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Text numberText;
    
    public void BombsSwitch(int index)
    {
        switch (index)
        {
            case 0: numberText.text = "0"; break;
            case 1: numberText.text = "1"; break;
            case 2: numberText.text = "2"; break;
        }
    }
}
