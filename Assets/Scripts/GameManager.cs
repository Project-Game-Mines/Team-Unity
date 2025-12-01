using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int betAmount = 1;
    public float totalCheckout = 0;
    public bool active = false;
    public int gameFase = 0;

    [SerializeField] private ButtonBet buttonBet;
    
    //[SerializeField] private BetButton betButton;
    [SerializeField] private MockPlayer player;
    [SerializeField] private GridManager gridManager;

   // [SerializeField] private BetAmount betAmount;
    

    
    public void SetGameActive()
    {
        active = true;
        

    }

    public void StartGame()
    {
        SetGameActive();
        DebitBalance();
        totalCheckout = betAmount;
        gridManager.UnlockGridMines();
        
    }

    public void DebitBalance()
    {
        player.balance -= betAmount;
    }

    public void GameOver()
    {
        active = false;
        gameFase = 0;
        gridManager.ResetMinesButtons();
        
    }

    
    
    
    
}
