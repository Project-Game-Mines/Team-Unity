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


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Ao clicar verifica se o jogo esta ativo, tanto no front quanto no back, se a mina individual esta ativa e verifica a resposta do WS se nao for mina chama WIN se for mina chama LOSE
    public void OnClickWinOrLose()
    {
        if (GameManager.match != null)
        {
            if (active && GameManager.match.active && gameManager.mineButtonActive && gameManager.active)
            {

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
        }
       


    // trata a situaçao se clicou numa mina, mostra o icone, efeito de lose e reseta o jogo
    public void PlayLoseAnimation()
    {
        PlayHitParticle();
        bombImage.SetActive(true);
        buttonBet.ImpossibleCashout();
        audioManager.BombSound();
        gameManager.CheckOutLose();
        buttonBet.RestartButtonBet();
    }
        // trata a situaçao se nao clicou em mina, mostra icone, animaçao e continua o jogo, bloqueando a mina de ser clicada de novo no mesmo jogo
    public void PlayWinAnimation()
    {
        animator.SetTrigger("Win");
        coinImage.SetActive(true);
        audioManager.FoundCoin();
        active = false;
        gameManager.gameFase += 1;
        buttonBet.UpdateCheckOutPrice();
    }
    //Ativa bomb anim
    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
        Destroy(instantiatedParticle.gameObject, instantiatedParticle.main.duration);
    }
    //Reseta o butao para o estado inicial
    public void ResetButtons()
    {
        animator.SetBool("Active", false);
        active = false;
        
    }
    //Reseta os icones de bomba e diamante
    public void ResetButtonsIcons()
    {
       
       bombImage.gameObject.SetActive(false);
       coinImage.gameObject.SetActive(false);
   }

    // Ativa os icones no final do jogo, nas minas que ainda nao foram mostradas na match
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
