using System;
using UnityEngine;

public class MineButtonBehavior : MonoBehaviour
{
    private bool active = false;
    private Animator animator;
    [SerializeField] private ParticleSystem hitParticle;
    [SerializeField] private GameObject bombImage;
    [SerializeField] private GameObject coinImage;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnClickWinOrLose()
    {
        if (active)
        {
            PlayWinAnimation();
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
    }
}
