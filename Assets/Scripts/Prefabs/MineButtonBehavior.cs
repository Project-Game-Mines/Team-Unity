using System;
using UnityEngine;

public class MineButtonBehavior : MonoBehaviour
{
    private Animator animator;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnClickWinOrLose()
    {
        PlayLoseAnimation();
    }



    private void PlayLoseAnimation()
    {
        animator.SetTrigger("Lose");
       
    }

    private void PlayWinAnimation()
    {
        animator.SetTrigger("Win");
    }
    
}
