using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioListener audioListener;
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
        audioListener.enabled = true;
    }

    public void AudioListenerOff()
    {
        audioListener.enabled = false;
    }

    private void SaveSoundState()
    {
        PlayerPrefs.SetInt("sound", (audioListener.enabled ? 1 : 0));
    }

    private void GetSoundState()
    {
        if (Manager.Instance.CheckFisrtStartGame())
        {
            AudioListenerOn();
        }
        else
            audioListener.enabled = (PlayerPrefs.GetInt("sound") != 0);

        SetSoundToggle(audioListener.enabled);
    }

    private void SetSoundToggle(bool value)
    {
        soundToggle.isOn = value;
    }

    public void CheckSound(bool checkmark)
    {
        if (checkmark)
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
