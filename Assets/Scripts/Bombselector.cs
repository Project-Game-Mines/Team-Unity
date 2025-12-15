using TMPro;
using UnityEngine;

public class Bombselector : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TMP_Dropdown dropdownSelector; 

    // Escolher a quantidade de minas
    public void SetBombAmount()
    {
        
        string selectedOptionText = dropdownSelector.options[dropdownSelector.value].text;

        if (int.TryParse(selectedOptionText, out int bombCount))
        {
            gameManager.bombAmount = bombCount;
            Debug.Log($"Quantidade de bombas definida para: {bombCount}");
        }
    }

    // desativa a funcionalidade de dropdown
    public void DeactivateDropdown()
    {
        if (dropdownSelector != null)
        {
            // ✅ AGORA CORRETO: A propriedade interactable está no componente TMP_Dropdown
            dropdownSelector.interactable = false;
            Debug.Log("Dropdown de seleção de bombas desativado.");
        }
    }
    
    // reativa o butao
    public void ActivateDropdown()
    {
        if (dropdownSelector != null)
        {
            dropdownSelector.interactable = true;
            Debug.Log("Dropdown de seleção de bombas reativado.");
        }
    }
}