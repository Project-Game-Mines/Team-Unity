using System;
using System.Collections;
using UnityEngine;

public class MineButtonBehavior : MonoBehaviour
{
    public bool active = false;
    public Animator animator;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject bombImage;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MockPlayer player;
    public int mineValue;

    [SerializeField] private ButtonBetTemp buttonBet;
    [SerializeField] private BetStateManager _betStateManager;
    [SerializeField] private BetUIController _betUIController;
    [SerializeField] private AudioManager audioManager;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void OnClickWinOrLose()
    {
        if (active)
        {
            if (player.mineList.Contains(mineValue))
            {
                PlayLoseAnimation();
            }
            else
            {
                PlayWinAnimation();
                _betStateManager.UpdateState();
            }
            
            Debug.Log($"Clicou na mina {mineValue}");
        }
        else
        {
            
        }
        
        
    }



    private void PlayLoseAnimation()
    {
        PlayHitParticle();
        bombImage.SetActive(true);
        audioManager.BombSound();
        gameManager.CheckOutLose();
        gameManager.GameOver();
        
        _betUIController.StartRestartSequence();
    }

    private void PlayWinAnimation()
    {
        animator.SetTrigger("Win");
        coinImage.SetActive(true);
        audioManager.FoundCoin();
        active = false;
        gameManager.gameFase += 1;
        gameManager.totalCheckout *= 1.2f;
        
        _betStateManager.UpdateCashoutValue();
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
        
            if (player.mineList.Contains(mineValue))
            {
                bombImage.SetActive(true);
            }
            else
            {
                coinImage.SetActive(true);
            }

    }


}
