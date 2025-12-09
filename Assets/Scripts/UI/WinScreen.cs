using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [Header("Prefab de win")]
    [SerializeField] private GameObject winScreen;

    private void Awake()
    {
        // Garante que comece desligado para evitar que apareça na hora errada
        if (winScreen != null) 
        {
            winScreen.SetActive(false);
        }
    }
    
    public void AtivarTela()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("WinScreen: O GameObject 'winScreen' não está associado no Inspector.");
        }
    }
    
    public void DesativarTela()
    {
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
    }
}