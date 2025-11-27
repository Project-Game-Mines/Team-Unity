using UnityEngine;

public class SoundOptions : MonoBehaviour
{
    private Animator animator;
    private bool soundActive = true;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnClickActivateSound()
    {
        if (soundActive == true)
        {
            soundActive = false;
            animator.SetBool("Sound", false);
        }
        else
        {
            soundActive = true;
            animator.SetBool("Sound", true);
        }
    }
}
