using UnityEngine;
using System.Collections;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    private Coroutine corrotinaAtual;
    
    public void AtivarPorTempo(float tempo = 3f)
    {
        if (corrotinaAtual != null)
        {
            StopCoroutine(corrotinaAtual);
        }
        
        corrotinaAtual = StartCoroutine(RotinaAtivarDesativar(tempo));
    }
    
    private IEnumerator RotinaAtivarDesativar(float tempoEspera)
    {
        winScreen.SetActive(true); 
        
        yield return new WaitForSeconds(tempoEspera);
        
        winScreen.SetActive(false);
        
        corrotinaAtual = null;
    }
}