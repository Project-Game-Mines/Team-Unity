using UnityEngine;

public class MineButtonBehavior : MonoBehaviour
{
    private Animator animator;
    public void OnClickWinOrLose()
    {
        animator.SetTrigger("win");
    }
    
}
