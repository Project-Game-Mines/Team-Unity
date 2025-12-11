using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundEffects : MonoBehaviour
{
    public AudioManager _audioManager;
    public Button buttonSoundEffects;
    public Animator buttonAnimator;
    public GameObject optionsPanel;

    public bool isMuted = false;
    //private static readonly int MutedParam = Animator.StringToHash("Sound"); // nome do parâmetro no Animator
    
    void Start()
    {
        // Liga o evento do botão
        buttonSoundEffects.onClick.AddListener(MuteSoundEffects);
        buttonAnimator.SetBool("Sound", isMuted);
    }

    void MuteSoundEffects()
    {
        // Inverte o estado local
        isMuted = !isMuted;

        // Aplica no AudioManager
        _audioManager.SetMute(isMuted);
        buttonAnimator.SetBool("Sound", isMuted);
    }

    public void OpenPanel()
    {
        optionsPanel.SetActive(true);
        if (!isMuted)
        {    buttonAnimator.SetBool("Sound", true);}
        else
        {
            buttonAnimator.SetBool("Sound", false);
        }
        
            
    }
    
    

    
}
