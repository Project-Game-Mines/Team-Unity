using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int betAmount = 1;
    public bool active = false;
    public bool gameOver = false;
    public int gameFase = 0;

    [SerializeField] private ButtonBet buttonBet;
    
    [SerializeField] private BetButton betButton;
    [SerializeField] private MockPlayer player;
    [SerializeField] private GridManager gridManager;

   // [SerializeField] private BetAmount betAmount;
    

    
    public void SetGameActive()
    {
        active = true;
        gameOver = false;

    }

    public void StartGame()
    {
        SetGameActive();
        DebitBalance();
        gridManager.UnlockGridMines();
        
    }

    public void DebitBalance()
    {
        player.balance -= betAmount;
    }

    public void GameOver()
    {
        active = false;
        gameOver = true;
        gameFase = 0;
        buttonBet.RestartButtonBet();
        gridManager.ResetMinesButtons();
        
    }

    
    
    
    
}
