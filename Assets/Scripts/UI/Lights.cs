using System.Collections;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    
    //Chama a função em loop do efeito de luz atras do GRID 
    void Start()
    {
        StartCoroutine(LightsSequenceLoop());
        
    }
    // A cada 0,5 Segundos troca as imagens atras do GRID para dar efeito de luzes piscando
    private IEnumerator LightsSequenceLoop()
    {
        // Cache de WaitForSeconds para evitar alocações contínuas
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while (true) // repete para sempre
        {
            light1.SetActive(true);
            light2.SetActive(false);
            light3.SetActive(false);
            yield return wait;

            light1.SetActive(false);
            light2.SetActive(true);
            light3.SetActive(false);
            yield return wait;

            light1.SetActive(false);
            light2.SetActive(false);
            light3.SetActive(true);
            yield return wait;
            
        }
    }
    
}