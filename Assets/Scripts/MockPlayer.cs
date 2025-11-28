
using System.Collections.Generic;
using UnityEngine;

public class MockPlayer : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    
    public int playerid = 123;
    public float balance = 5000.45f;
    public List<int> mineList = new();


    void Start()
    {
        CratingRandomMines();
    }
    public void CratingRandomMines()
    {
        const int limiteMaximoExclusivo = 25;
        for (int i = 0; i < 3; i++)
        {
            int numeroAleatorio = Random.Range(0, limiteMaximoExclusivo);
            mineList.Add(numeroAleatorio);
        }
    }

}
