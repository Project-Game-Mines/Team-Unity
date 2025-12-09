using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class MineButtonBehavior : MonoBehaviour
{
    public bool active = false;
    public Animator animator;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject bombImage;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private GameManager gameManager;
    
    public int mineValue;

    [SerializeField] private ButtonBet buttonBet;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameWebSocket gameWebSocket;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void OnClickWinOrLose()
    {
        if (active && GameManager.match.active && gameManager.mineButtonActive && gameManager.active)
        {
            // **REMOVE A LÓGICA ANTIGA BASEADA NA LISTA LOCAL (player.mineList.Contains)**

            // 1. Chama GameStep, passando a lógica de processamento da resposta no callback
            gameWebSocket.GameStep(GameManager.match.matchId, mineValue, (response) =>
            {
                // Este bloco de código só será executado quando a resposta chegar do servidor!
            
                try
                {
                    JObject jsonResponse = JObject.Parse(response);
                    string eventType = jsonResponse["event"].ToString();
                
                    // 2. Verifica o tipo de evento (GAME_LOSE ou STEP_RESULT)
                    if (eventType == "GAME_LOSE")
                    {
                        // Confirmação do servidor: O clique atingiu uma mina
                        PlayLoseAnimation();
                        gameManager.mineButtonActive = true;
                    }
                    else if (eventType == "STEP_RESULT")
                    {
                        // Confirmação do servidor: O clique foi em uma célula segura
                        PlayWinAnimation();
                        gameManager.mineButtonActive = true;
                        buttonBet.PossibleCashout();
                    }
                    else
                    {
                        Debug.LogWarning($"Evento de resposta inesperado do servidor: {eventType}");
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Erro ao analisar resposta JSON do GameStep: {e.Message}");
                }
            });
        
            Debug.Log($"Chamada GameStep enviada para a célula {mineValue}. Aguardando resposta do WS...");
        }           
        else
        {
            Debug.Log("Game Not started");
        }
        
        
    }



    public void PlayLoseAnimation()
    {
        PlayHitParticle();
        bombImage.SetActive(true);
        buttonBet.ImpossibleCashout();
        audioManager.BombSound();
        gameManager.CheckOutLose();
        buttonBet.RestartButtonBet();
    }

    public void PlayWinAnimation()
    {
        animator.SetTrigger("Win");
        coinImage.SetActive(true);
        audioManager.FoundCoin();
        active = false;
        gameManager.gameFase += 1;
        gameManager.totalCheckout *= 1.2f;
        buttonBet.UpdateCheckOutPrice();
    }
    
    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
        Destroy(instantiatedParticle.gameObject, instantiatedParticle.main.duration);
    }

    public void ResetButtons()
    {
        animator.SetBool("Active", false);
        active = false;
        
    }

    public void ResetButtonsIcons()
    {
       
       bombImage.gameObject.SetActive(false);
       coinImage.gameObject.SetActive(false);
   }

    public void ShowIconsEndGame()
    {
        
            if (GameManager.minesPositions.mines_positions.Contains(mineValue))
            {
                bombImage.SetActive(true);
            }
            else
            {
                coinImage.SetActive(true);
            }

    }


}
