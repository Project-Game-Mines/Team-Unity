using System.Collections;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [Header("Prefab de win")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] public TextMeshProUGUI winText;

    private void Awake()
    {
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
            Debug.Log("Activate WinScreen2");
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