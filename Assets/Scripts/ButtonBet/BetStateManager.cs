using UnityEngine;
using System;

public enum BetButtonState
{
    ReadyToBet,      // Laranja, "BET"
    WaitingCashout,  // Vermelho claro, desabilitado
    CanCashout       // Vermelho, "CASHOUT"
}

public class BetStateManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    
    public event Action<BetButtonState> OnStateChanged;
    public event Action<float> OnCashoutValueChanged;
    
    private BetButtonState _currentState;

    public BetButtonState CurrentState
    {
        get { return _currentState; }
    }

    void Start()
    {
        UpdateState();
    }

    // Atualiza o estado do botão com base no jogo
    public void UpdateState()
    {
        BetButtonState newState;

        if (!_gameManager.active)
        {
            newState = BetButtonState.ReadyToBet;
        } 
        else if (_gameManager.gameFase > 0)
        {
            newState = BetButtonState.CanCashout;
        }
        else
        {
            newState = BetButtonState.WaitingCashout;
        }
            

        if (newState != _currentState)
        {
            _currentState = newState;

            if (OnStateChanged != null)
            {
                OnStateChanged.Invoke(_currentState);
            }
                
        }
    }

    // verifica se o jogador pode apostar e inicia o jogo caso seja permitido.
    // Se a aposta for aceita, atualiza o estado e dispara o evento com o valor inicial de cashout.
    public bool TryPlaceBet()
    {
        if (!_gameManager.CheckIfCanPlay())
        {
            return false;
        } 

        _gameManager.StartGame();
        UpdateState();

        if (OnCashoutValueChanged != null)
        {
            OnCashoutValueChanged.Invoke(_gameManager.totalCheckout);
        }    

        return true;
    }

    public void ExecuteCashout()
    {
        if (!_gameManager.active)
        {
            return;
        }
             
        _gameManager.CheckOutWin();
        UpdateState();
    }
    
    public void UpdateCashoutValue()
    {
        if (_currentState == BetButtonState.CanCashout)
        {
            if (OnCashoutValueChanged != null)
            {
                OnCashoutValueChanged.Invoke(_gameManager.totalCheckout);
            }
        }
    }
    
    public void NotifyGameRestarted()
    {
        UpdateState();
    }
}