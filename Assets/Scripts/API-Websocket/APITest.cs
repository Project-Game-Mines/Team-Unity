using UnityEngine;
using TMPro; // Necessário para TextMeshPro

public class PlayerDisplay : MonoBehaviour
{
    // Arraste e solte o APIManager (do GameObject) aqui no Inspector
    [SerializeField] private APIManager apiManager; 
    
    // Arraste e solte seu componente TextMeshProUGUI aqui no Inspector
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI balanceText;

    // Método a ser chamado quando os dados estiverem prontos
    public void UpdatePlayerDisplay()
    {
        // 1. Checa se o APIManager e o Player foram carregados
        if (apiManager != null && GameManager.player != null)
        {
            // 2. Exibe o nome
            string playerName = GameManager.player.name;
            
            if (playerNameText != null)
            {
                playerNameText.text = "Nome Carregado: " + playerName;
            }
            
            Debug.Log("Dados Carregados! Nome do Jogador: " + playerName);
        }
        else
        {
            Debug.LogError("Dados do APIManager ou do Player estão faltando.");
        }
    }

    public void UpdateBalance()
    {
        balanceText.text = GameManager.player.balance.ToString("F2");
    }
}