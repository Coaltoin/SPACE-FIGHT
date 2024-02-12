using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioMixer masterMixer;
    [SerializeField] Slider soundSlider;

    public AudioClip startMusic;
    public AudioClip gameMusic;
    public AudioClip playerAttack;
    public AudioClip playerHit;
    public AudioClip enemyHit;

    public static AudioManager audioManager;

    private void Start() 
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        musicSource.clip = startMusic;
        musicSource.Play();
        audioManager = this;
    }

    public void SetVolume(float _value) 
    {
        if (_value < 1) 
        {
            _value = .001f;
        }

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime, int musicID)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        switch(musicID)
        {
            case 0:
                audioSource.clip = startMusic;
                break;
            case 1:
                audioSource.clip = gameMusic;
                break;
            default:
                audioSource.clip = startMusic;
                break;
        }
        audioSource.Play();
    }

    public void ChangeMusic(int id)
    {
        StartCoroutine(FadeOut(musicSource, 0.3f, id));
    }

    public void SetVolumeFromSlider() 
    {
        SetVolume(soundSlider.value);
    }

    public void RefreshSlider(float _value) 
    {
        soundSlider.value = _value;
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}


