using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundEffects : MonoBehaviour
{
    public AudioManager _audioManager;
    public Button buttonSoundEffects;

    private bool state = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonSoundEffects.onClick.AddListener(MuteSoundEffects);
    }

    void MuteSoundEffects()
    {
        if (state)
        {
            _audioManager.SetMute(state); // true = mutado
            state = false;
        }
        else
        {
            _audioManager.SetMute(state);
            state = true;
        }
    }

    
}
