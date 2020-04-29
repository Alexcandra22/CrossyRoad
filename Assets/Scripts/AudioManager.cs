using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Toggle soundToggle;
    public AudioSource[] allAudioSources;

    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

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

    private void Start()
    {
        GetSoundState();
    }

    public void StopAllAudio()
    {
        foreach (var audio in allAudioSources)
        {
            audio.Stop();
        }

        AudioListenerOff();
    }

    public void AudioListenerOn()
    {
        AudioListener.pause = false;
    }

    public void AudioListenerOff()
    {
        AudioListener.pause = true;
    }

    private void SaveSoundState()
    {
        PlayerPrefs.SetInt("audio", (AudioListener.pause ? 1 : 0));
    }

    private void GetSoundState()
    {
        if (Manager.Instance.CheckFisrtStartGame())
            AudioListenerOn();
        else
            AudioListener.pause = (PlayerPrefs.GetInt("audio") != 0);
       
        SetSoundToggle(!AudioListener.pause);
    }

    private void SetSoundToggle(bool value)
    {
        soundToggle.isOn = value;
    }

    public void CheckSound()
    {
        if (soundToggle.isOn)
            AudioListenerOn();
        else
            AudioListenerOff();

        SaveSoundState();
    }

    public AudioSource GetAudio(string nameAudio)
    {
        foreach (var currentAudio in allAudioSources)
        {
            if (nameAudio == currentAudio.tag)
                return currentAudio;
        }

        return null;
    }
}
