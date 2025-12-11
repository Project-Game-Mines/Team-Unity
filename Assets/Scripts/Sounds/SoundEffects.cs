using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundEffects : MonoBehaviour
{
    public AudioManager _audioManager;
    public Button buttonSoundEffects;
    public Animator animator;
    public GameObject optionsPanel;
    [SerializeField] private SoundBackground  soundBackground;

    public bool isMuted = false;
    
    void Start()
    {
        // Liga o evento do botão
        buttonSoundEffects.onClick.AddListener(MuteSoundEffects);
        animator.SetBool("Sound", isMuted);
    }

    void MuteSoundEffects()
    {
        // Inverte o estado local
        isMuted = !isMuted;

        // Aplica no AudioManager
        _audioManager.SetMute(isMuted);
        animator.SetBool("Sound", isMuted);
    }

    public void OpenPanel()
    {
        CheckSoundEffects();
        soundBackground.CheckSoundBackground();
    }

    private void CheckSoundEffects()
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
