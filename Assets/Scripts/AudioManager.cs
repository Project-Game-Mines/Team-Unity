using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip cashout;
    public AudioClip bomb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CashoutSound()
    {
        audioSource.PlayOneShot(cashout, 2.0f);
    }

    public void BombSound()
    {
        audioSource.PlayOneShot(bomb, 2.0f);
    }
}
