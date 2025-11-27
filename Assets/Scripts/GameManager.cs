using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int betAmount = 1;
    public bool active = false;

   // [SerializeField] private BetAmount betAmount;
    

    
    public void SetGameActive()
    {
        active = true;
    }

    public void StartGame()
    {
        SetGameActive();
        
    }

    
}
