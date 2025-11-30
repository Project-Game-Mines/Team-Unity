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

    [SerializeField] private ButtonBet buttonBet;

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
                buttonBet.PossibleCashout();
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
        gameManager.GameOver();
        buttonBet.RestartButtonBet();
    }

    private void PlayWinAnimation()
    {
        animator.SetTrigger("Win");
        coinImage.SetActive(true);
        active = false;
        gameManager.gameFase +=1;
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

    
}
