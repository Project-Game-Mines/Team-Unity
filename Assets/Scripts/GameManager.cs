using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Match match;
    public static Player player;
    public static MatchStep matchStep;
    public static MinesPosition minesPositions;
    public int betAmount = 1;
    public int bombAmount;
    public float totalCheckout = 0;
    public bool active = false;
    public int gameFase = 0;
    [SerializeField] private APIManager apiManager;
    [SerializeField] private GameWebSocket  gameWebSocket;

    [SerializeField] private ButtonBet buttonBet;
    
    //[SerializeField] private BetButton betButton;
    [SerializeField] private MockPlayer mockPlayer;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Bombselector Bombselector;
    
    
    
    

   // [SerializeField] private BetAmount betAmount;

   private void Awake()
   {
       apiManager.StartFetchingPlayer();
       
   }

   public void SetGameActive()
    {
        active = true;
        

    }

    public void StartGame()
    {
            Bombselector.SetBombAmount();
            gameWebSocket.StartGame("692f1d6cedc0062c96dd0dc5", betAmount, bombAmount);
            //apiManager.RequestStartGame("692f1d6cedc0062c96dd0dc5", betAmount, bombAmount);
            mockPlayer.StartGame();
            SetGameActive();
            DebitBalance();
            totalCheckout = betAmount;
            gridManager.UnlockGridMines();

        
    }

    public void DebitBalance()
    {
        mockPlayer.balance -= betAmount;
    }

    public void GameOver()
    {
        active = false;
        gameFase = 0;
        gridManager.ResetMinesButtons();

    }

    public void CheckOutWin()
    {
        mockPlayer.balance += totalCheckout;
        GameOver();
    }

    public void CheckOutLose()
    {
        totalCheckout = 0;
    }

    public bool CheckIfCanPlay()
    {
        
        if (betAmount <= player.balance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateBalance()
    {
        apiManager.UpdatePlayerBalance();
    }
}
