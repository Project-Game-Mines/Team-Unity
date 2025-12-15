using System.Collections;
using UnityEngine;

public class Lights : MonoBehaviour
{
    [Header("Luz Giratória")]
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    
    [Header("Pontos de luz amarelo")]
    public GameObject pointY1;
    public GameObject pointY2;
    public GameObject pointY3;
    public GameObject pointY4;
    public GameObject pointY5;
    public GameObject pointY6;
    public GameObject pointY7;
    public GameObject pointY8;

    [Header("Pontos de luz vermelho")]
    public GameObject pointR1;
    public GameObject pointR2;
    public GameObject pointR3;
    public GameObject pointR4;
    public GameObject pointR5;
    public GameObject pointR6;
    public GameObject pointR7;
    public GameObject pointR8;
    public GameObject pointR9;
    public GameObject pointR10;
    public GameObject pointR11;
    public GameObject pointR12;
    
    //Chama a função em loop do efeito de luz atras do GRID 
    void Start()
    {
        StartCoroutine(LightsSequenceLoop());
        StartCoroutine(LightsSequenceYellows());
        StartCoroutine(LightsSequenceReds());
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

    private IEnumerator LightsSequenceYellows()
    {
        WaitForSeconds wait = new WaitForSeconds(0.6f);

        while (true) // repete para sempre
        {
            pointY1.SetActive(true);
            pointY3.SetActive(true);
            pointY5.SetActive(true);
            pointY7.SetActive(true);
            pointY2.SetActive(false);
            pointY4.SetActive(false);
            pointY6.SetActive(false);
            pointY8.SetActive(false);
            yield return wait;

            pointY1.SetActive(false);
            pointY3.SetActive(false);
            pointY5.SetActive(false);
            pointY7.SetActive(false);
            pointY2.SetActive(true);
            pointY4.SetActive(true);
            pointY6.SetActive(true);
            pointY8.SetActive(true);
            yield return wait;
        }
    }
    
    private IEnumerator LightsSequenceReds()
    {
        // Cache de WaitForSeconds para evitar alocações contínuas
        WaitForSeconds wait = new WaitForSeconds(1f);

        while (true) // repete para sempre
        {
            pointR1.SetActive(true);
            pointR2.SetActive(false);
            pointR3.SetActive(false);
            
            pointR4.SetActive(true);
            pointR5.SetActive(false);
            pointR6.SetActive(false);
            
            pointR7.SetActive(true);
            pointR8.SetActive(false);
            pointR9.SetActive(false);
            
            pointR10.SetActive(true);
            pointR11.SetActive(false);
            pointR12.SetActive(false);
            yield return wait;

            pointR1.SetActive(false);
            pointR2.SetActive(true);
            pointR3.SetActive(false);
            
            pointR4.SetActive(false);
            pointR5.SetActive(true);
            pointR6.SetActive(false);
            
            pointR7.SetActive(false);
            pointR8.SetActive(true);
            pointR9.SetActive(false);
            
            pointR10.SetActive(false);
            pointR11.SetActive(true);
            pointR12.SetActive(false);
            yield return wait;

            pointR1.SetActive(false);
            pointR2.SetActive(false);
            pointR3.SetActive(true);
            
            pointR4.SetActive(false);
            pointR5.SetActive(false);
            pointR6.SetActive(true);
            
            pointR7.SetActive(false);
            pointR8.SetActive(false);
            pointR9.SetActive(true);
            
            pointR10.SetActive(false);
            pointR11.SetActive(false);
            pointR12.SetActive(true);
            yield return wait;
            
        }
    }

}