using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool Paused = false;
    public GameObject PauseMenuCanvas;
    public GameObject PauseButton;

    public AudioSource PauseSound;
    public AudioSource UnPauseSound;
    public AudioSource OptionSelectedSound;

    void Start() 
    {
        Time.timeScale = 1f;
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(Paused) 
            {
                Play();
            }
            else 
            {
                Stop();
            }
        }
    }

    public void PlaySelectSound()
    {
        OptionSelectedSound.Play();
    }

    public void Stop() 
    {
       // GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Pause();
        PauseSound.Play();
        //OptionSelectedSound.Play();
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        PauseButton.SetActive(false);
    }

    public void Play() 
    {
    //   GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Play();
        UnPauseSound.Play();
       // OptionSelectedSound.Play();
        PauseButton.SetActive(true);
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    public void MainMenuButton() 
    {
        OptionSelectedSound.Play();
        SceneManager.LoadScene("MainMenu");
    }

}

