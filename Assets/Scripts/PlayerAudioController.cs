using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip moveSound;
    private AudioSource audioSource;

    private static PlayerAudioController instance;
    public static PlayerAudioController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void GetAudioPlayerDeath()
    {
        audioSource.PlayOneShot(deathSound, 1);
    }

    public void GetAudioPlayerMove()
    {
        audioSource.PlayOneShot(moveSound, 1);
    }
}
