using UnityEngine;
using UnityEngine.UI;

public class SoundBackground : MonoBehaviour
{
    public AudioManager _audioManager;
    public Button buttonBackgroundSound;
    public GameObject optionsPanel;
    public Animator animator;
    //public SoundEffects soundEffects;
    
    private AudioSource audioSourceBackground;
    public AudioClip soundBackground;
    
    public bool isMuted;

    void Start()
    {
        audioSourceBackground = GetComponent<AudioSource>();
        
        audioSourceBackground.clip = soundBackground;
        audioSourceBackground.loop = true;
        audioSourceBackground.Play();
        
        // Liga o evento do botão
        buttonBackgroundSound.onClick.AddListener(MuteSoundEffects);
        animator.SetBool("Sound", isMuted);
    }
    
    void MuteSoundEffects()
    {
        // Inverte o estado local
        isMuted = !isMuted;

        // Aplica no AudioManager
        SetMute(isMuted);
        animator.SetBool("Sound", isMuted);
    }
    
    public void SetMute(bool mute)
    {
        audioSourceBackground.mute = mute;
    }
    
    public void CheckSoundBackground()
    {
        optionsPanel.SetActive(true);
        if (!isMuted)
        {    animator.SetBool("Sound", true);}
        else
        {
            animator.SetBool("Sound", false);
        }
    }
    
    
    
}
