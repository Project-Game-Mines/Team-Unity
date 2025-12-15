using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Match match;
    public static Player player;
    public static MatchStep matchStep;
    public static MinesPosition minesPositions;
    public bool mineButtonActive = false;
    public int betAmount = 1;
    public int bombAmount;
    public float totalCheckout = 0;
    public bool active = false;
    public int gameFase = 0;
    [SerializeField] private APIManager apiManager;
    [SerializeField] private GameWebSocket  gameWebSocket;
    [SerializeField] private WinScreen winScreen;

    [SerializeField] private ButtonBet buttonBet;
    
    //[SerializeField] private BetButton betButton;
    
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Bombselector Bombselector;

    [SerializeField] private Lights lights;
    

   
    // No começo do jogo usa API para dar fetch Player
   private void Start()
   {
       apiManager.StartFetchingPlayer();
       
   }

   //Ativa o jogo no front (ainda presicando de validaçao a parte do back)
   public void SetGameActive()
    {
        active = true;
        

    }
    //Começar o jogo, valida a  quantidade de minas, manda começo da match com o WS, coloca a quanitade de cashout inicial, libera a GRID
    public void StartGame()
    {
            Bombselector.SetBombAmount();
            gameWebSocket.StartGame("6940215313b9fbbb56560abd", betAmount, bombAmount);
            SetGameActive();
            totalCheckout = betAmount;
            gridManager.UnlockGridMines();
            lights.GameIsRunning();

        
    }

    
    // Acaba o jogo, trava a grid, desativa o jogo no front, Reseta A grid, e a classe match e matchStep sao apagadas
    public void GameOver()
    {
        mineButtonActive = false;
        active = false;
        gameFase = 0;
        gridManager.ResetMinesButtons();
        match = null;
        matchStep = null;
        lights.GameNoRunning();
        

    }
    //trata o CashOUT com WS
    public void CheckOutWin()
    {
        gameWebSocket.ChashOut(match.matchId);
        
    }
    //zera a quantidade local de cashout
    public void CheckOutLose()
    {
        totalCheckout = 0;
    }
    //verifica se tem saldo o suficiente para jogar
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
    // atualiza o valor no front com o valor do back com API
    public void UpdateBalance()
    {
        apiManager.UpdatePlayerBalance();
    }

    public void UpdatePrizeStart()
    {
        matchStep.prize = betAmount;
        
    }

    //Butao de BET volta para o estado inicial
    public void WaitChasoutWS()
    {
        buttonBet.RestartButtonBet();
        
    }
    // Ativa a tela de WIN
    public void ActivateWinScreen()
    {
        StartCoroutine(WinScreenCashOut());
    }
    //desativa a tela de win(usada pelo propio unity)
    public void DeactivateWinScreen()
    {
        winScreen.DesativarTela();
    }
    //Atualiza o valor mostrado de cashout no winscreen
    public void UpdateWinScreenCashOut()
    {
        winScreen.winText.text = totalCheckout.ToString();
    }
    //espera as minas e diamantes aparecer antes de mostrar a tela de WIN
    private IEnumerator WinScreenCashOut()
    {
        yield return new WaitForSeconds(2);
        winScreen.AtivarTela();
        winScreen.MostrarValorAnimado(totalCheckout, "R$","");
        UpdateBalance();
        Debug.Log("Activate WinScreen");
    }


}
