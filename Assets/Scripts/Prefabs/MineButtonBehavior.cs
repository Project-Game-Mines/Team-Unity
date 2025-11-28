using System;
using System.Collections;
using UnityEngine;

public class MineButtonBehavior : MonoBehaviour
{
    private bool active = false;
    private bool canActivate = true;
    private Animator animator;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject bombImage;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MockPlayer player;
    public int mineValue;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameManager.active == true && canActivate)
        {
            animator.SetBool("Active", true);
            active = true;
            canActivate=false;
        }

        if (gameManager.active == false)
        {
            active = false;
        }

        if (gameManager.gameOver)
        {
            animator.SetBool("Active", false);
            StartCoroutine(CloseIcons());
            StartCoroutine(HandleGameOver());

        }

        
        
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
            }
            
            Debug.Log($"Clicou na mina {mineValue}");
        }
        
        
    }



    private void PlayLoseAnimation()
    {
        PlayHitParticle();
        bombImage.SetActive(true);
        gameManager.GameOver();
        StartCoroutine(HandleGameOver());
        
    }

    private void PlayWinAnimation()
    {
        animator.SetTrigger("Win");
        coinImage.SetActive(true);
        active=false;
        gameManager.gameFase +=1;
        
        
    }
    
    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
        Destroy(instantiatedParticle.gameObject, instantiatedParticle.main.duration);
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(3f);
        canActivate = true;
    }

    private IEnumerator CloseIcons()
    {
        yield return new WaitForSeconds(3f);
        bombImage.gameObject.SetActive(false);
        coinImage.gameObject.SetActive(false);
    }

    
}
