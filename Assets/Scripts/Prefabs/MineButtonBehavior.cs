using System;
using UnityEngine;

public class MineButtonBehavior : MonoBehaviour
{
    private bool active = false;
    private Animator animator;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject bombImage;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private GameManager gameManager;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameManager.active == true)
        {
            animator.SetBool("Active", true);
            active = true;
        }
    }
    public void OnClickWinOrLose()
    {
        if (active)
        {
            PlayLoseAnimation();
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
