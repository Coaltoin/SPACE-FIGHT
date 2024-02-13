using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] AudioMixer masterMixer;
    
    private void Start() 
    {
       // SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
        SetVolume(60f);
    }

    public void SetVolume(float _value) 
    {
        
        AudioListener.volume = soundSlider.value / 30;

      /*
        if (_value < 1) 
        {
            _value = .001f;
        }

       // Debug.Log(_value);

        RefreshSlider(_value);
        PlayerPrefs.SetFloat("SavedMasterVolume", _value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(_value / 100) * 20f);
        */
    }
   
    public void SetVolumeFromSlider() 
    {
        SetVolume(soundSlider.value);
       // AudioListener.volume = soundSlider.value;
    }

    public void RefreshSlider(float _value) 
    {
        soundSlider.value = _value;
    }
    
}
