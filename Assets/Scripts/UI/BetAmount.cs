using System;
using TMPro;
using UnityEngine;

public class BetAmount : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI betAmountText;
    [SerializeField] private GameManager _gameManager;


    private void Awake()
    {
        betAmountText.text = _gameManager.betAmount.ToString();
    }

    public void AddOneBetAmount()
    {
        if (!_gameManager.active)
        {
            if (_gameManager.betAmount < 500)
            {
                _gameManager.betAmount += 1;
                betAmountText.text = _gameManager.betAmount.ToString();
            }
        }
        
        
    }
    public void MinusOneBetAmount()
    {
        if (!_gameManager.active)
        {
            if (_gameManager.betAmount >= 2)
            {
                _gameManager.betAmount -= 1;
                betAmountText.text = _gameManager.betAmount.ToString();
            }
        }
    }
    public void DobleBetAmount()
    {
        if (!_gameManager.active)
        {
            _gameManager.betAmount *= 2;
            if (_gameManager.betAmount > 500)
            {
                _gameManager.betAmount = 500;
            }

            betAmountText.text = _gameManager.betAmount.ToString();
        }
    }
    
    public void HalveBetAmount()
    {
        if (!_gameManager.active)
        {
            _gameManager.betAmount /= 2;
            if (_gameManager.betAmount < 1)
            {
                _gameManager.betAmount = 1;
            }

            betAmountText.text = _gameManager.betAmount.ToString();
        }
    }
    
}
