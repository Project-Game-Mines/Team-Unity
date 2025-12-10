using System.Collections;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;
    public GameObject farol1;
    public GameObject farol2;

    void Start()
    {
        StartCoroutine(LightsSequenceLoop());
        StartCoroutine(LightsFarolLoop());
    }

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
    
    private IEnumerator LightsFarolLoop()
    {
        // Cache de WaitForSeconds para evitar alocações contínuas
        WaitForSeconds wait = new WaitForSeconds(0.6f);

        while (true) // repete para sempre
        {
            farol1.SetActive(true);
            farol2.SetActive(false);
            yield return wait;

            farol1.SetActive(false);
            farol2.SetActive(true);
            yield return wait;
        }
    }
}