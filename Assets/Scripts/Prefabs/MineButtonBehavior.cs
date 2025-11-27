using System;
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
    }
    public void OnClickWinOrLose()
    {
        if (active)
        {
            PlayLoseAnimation();
            active=false;
            Debug.Log($"Clicou na mina {mineValue}");
        }
        
        
    }



    private void PlayLoseAnimation()
    {
        animator.SetTrigger("Lose");
        PlayHitParticle();
        bombImage.SetActive(true);  
        

    }

    private void PlayWinAnimation()
    {
        animator.SetTrigger("Win");
        coinImage.SetActive(true);
        
    }
    
    private void PlayHitParticle()
    {
        ParticleSystem instantiatedParticle = Instantiate(hitParticle, transform.position, transform.rotation);
        instantiatedParticle.Play();
        Destroy(instantiatedParticle.gameObject, instantiatedParticle.main.duration);
    }

    
}
