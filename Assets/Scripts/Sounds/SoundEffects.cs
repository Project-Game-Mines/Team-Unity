using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundEffects : MonoBehaviour
{
    public AudioManager _audioManager;
    public Button buttonSoundEffects;
    public Animator buttonAnimator;

    private bool isMuted = false;
    private static readonly int MutedParam = Animator.StringToHash("Sound"); // nome do parâmetro no Animator
    
    void Start()
    {
        // Atualiza animação do botão
        if (buttonAnimator != null)
        {
            buttonAnimator.SetBool(MutedParam, isMuted);
        }
        
        // Liga o evento do botão
        buttonSoundEffects.onClick.AddListener(MuteSoundEffects);

        
    }

    void MuteSoundEffects()
    {
        // Inverte o estado local
        isMuted = !isMuted;

        // Aplica no AudioManager
        _audioManager.SetMute(isMuted);
        
        // Atualiza animação do botão
        if (buttonAnimator != null)
        {
            buttonAnimator.SetBool(MutedParam, isMuted);
        }
        
    }
    
    

    
}
