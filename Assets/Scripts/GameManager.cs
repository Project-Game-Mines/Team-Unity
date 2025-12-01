using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int betAmount = 1;
    public int bombAmount;
    public float totalCheckout = 0;
    public bool active = false;
    public int gameFase = 0;

    [SerializeField] private ButtonBet buttonBet;
    
    //[SerializeField] private BetButton betButton;
    [SerializeField] private MockPlayer player;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Bombselector Bombselector;

   // [SerializeField] private BetAmount betAmount;
    

    
    public void SetGameActive()
    {
        active = true;
        

    }

    public void StartGame()
    {
            Bombselector.SetBombAmount();
            player.StartGame();
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

    public void CheckOutWin()
    {
        player.balance += totalCheckout;
        GameOver();
    }

    public void CheckOutLose()
    {
        totalCheckout = 0;
    }

    public bool CheckIfCanPlay()
    {
        
        if (betAmount > player.balance)
        {
            return false;
        }
        else
        {
            return true;
        }

        
    }
    
    
    
}
