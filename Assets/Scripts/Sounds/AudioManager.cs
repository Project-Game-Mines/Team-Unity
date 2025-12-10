using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip cashout;
    public AudioClip foundBomb;
    public AudioClip betClick;
    public AudioClip foundCoin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMute(bool mute)
    {
        audioSource.mute = mute;
    }


    public void CashoutSound()
    {
        audioSource.PlayOneShot(cashout, 2.0f);
    }

    public void BombSound()
    {
        audioSource.PlayOneShot(foundBomb, 2.0f);
    }

    public void FoundCoin()
    {
        audioSource.PlayOneShot(foundCoin, 2.0f);
    }

    public void BetClick()
    {
        audioSource.PlayOneShot(betClick, 2.0f);
    }
}
