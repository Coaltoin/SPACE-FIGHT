using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    private int AmountNeededToWinLvl1 = 80; // Amount needed to pass level 1
    private int AmountNeededToWinLvl2 = 120; // Amount needed to pass level 2

    private int CurrentAmount; // Amount player has accumilated thus far in order to pass the win condition (Can be any value stat as long as its an int)
    private int CurrentLevel = 1; // Current Lvl player is in

    [Header("Menu Settings")]
    public TextMeshProUGUI LevelScreen;
    public GameObject WinScreen;
    public AudioSource WinSound;
    public GameObject LoseScreen;
    public AudioSource LoseSound;
    public GameObject ReplayMenu;
    public GameObject HUDMenu;
    public GameObject ControlsMenu;

    private GameObject Player;
    private float FinalScore;

    private void Start()
    {
        CurrentAmount = 0;

        Player = GameObject.Find("Player!");
        

        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            PlayerPrefs.SetInt("CurrentLevel", 1);
            CurrentLevel = 1;
            Player.GetComponent<CollectUpgrade>().score = 0;
            Debug.Log("Player is in Level 1");
        } else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            RecordPlayerPosition(true);
            CurrentLevel = 2;

            Debug.Log("Loaded Previous Data.");

            Debug.Log("Player is in Level 2");
        }
    }

    private void Update()
    {
        UpdateLevelUI();
    }

    public void RecordPlayerPosition(bool Playback)
    {
        if (Player)
        {
            if (Playback == true)
            {
                float playerPositionX = PlayerPrefs.GetFloat("PlayerPositionX");
                float playerPositionY = PlayerPrefs.GetFloat("PlayerPositionY");

                Player.transform.position = new Vector3(playerPositionX, playerPositionY, 0);
            } else
            {
                float playerPositionX = Player.transform.position.x;
                float playerPositionY = Player.transform.position.y;

                PlayerPrefs.SetFloat("PlayerPositionX", playerPositionX);
                PlayerPrefs.SetFloat("PlayerPositionY", playerPositionY);
            }
          
        }
    }

    #region Win Condition Handlers
    /// <summary>
    /// Updates Level UI
    /// </summary>
    private void UpdateLevelUI()
    {
        if (LevelScreen)
        {
            LevelScreen.text = "Level " + CurrentLevel;
        }
    }

    /// <summary>
    /// Checks Current amount to see if win condition has been met
    /// </summary>
    private void CheckWinCondition()
    {
        int LastLevel = CurrentLevel;
        if (CurrentLevel == 1)
        {
            if (CurrentAmount >= AmountNeededToWinLvl1)
            {
                CurrentLevel += 1;
                FinalScore = Player.GetComponent<CollectUpgrade>().score;
                PlayerPrefs.SetFloat("Score", FinalScore);
                PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
                RecordPlayerPosition(false);
                SceneManager.LoadScene("Level " + CurrentLevel);
                Debug.Log("Completed Level " + LastLevel + ". Now Proceeding to Level " + CurrentLevel);
            }
        } else if (CurrentLevel == 2)
        {
            if (CurrentAmount >= AmountNeededToWinLvl2)
            {
                DidPlayerBeatGame(true);     
            }
        }
    }

    /// <summary>
    /// Add to current amount for win condition
    /// </summary>
    /// <param name="Amount"></param>
    public void AddCurrentAmount(int Amount)
    {
        CurrentAmount += Amount;

        if (CurrentLevel == 1)
        {
            Debug.Log("Player has increased Current Amount to " + CurrentAmount + ". Win Condition is " + AmountNeededToWinLvl1);
        } else if (CurrentLevel == 2)
        {
            Debug.Log("Player has increased Current Amount to " + CurrentAmount + ". Win Condition is " + AmountNeededToWinLvl2);
        }

        CheckWinCondition();
    }

    /// <summary>
    /// gets the current amount to check progress
    /// </summary>
    /// <returns></returns>
    public int GetCurrentAmount()
    {
        return CurrentAmount;
    }

    public int GetAmountNeededToWin_Lvl_1()
    {
        return AmountNeededToWinLvl1;
    }
    public int GetAmountNeededToWin_Lvl_2()
    {
        return AmountNeededToWinLvl2;
    }

    /// <summary>
    /// Did the player beat the game pass true if did false if didn't will display win/lose screen and stop the game
    /// </summary>
    /// <param name="Status"></param>
    public void DidPlayerBeatGame(bool Status)
    {
        Time.timeScale = 0;
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Stop();

        ReplayMenu.SetActive(true);
        ControlsMenu.SetActive(false);
        HUDMenu.SetActive(false);

        if (Status == true)
        {
            if (WinSound)
            {
                WinSound.Play();
            }

            if (WinScreen)
            {
                WinScreen.SetActive(true);
            }
            if (LoseScreen)
            {
                LoseScreen.SetActive(false);
            }   
            Debug.Log("Player has won!");
        } else
        {     
            Player.SetActive(false);
            if (LoseSound)
            {
                LoseSound.Play();
            }

            if (LoseScreen)
            {
                LoseScreen.SetActive(true);
            }
            if (WinScreen)
            {
                WinScreen.SetActive(false);
            }
            Debug.Log("Player has Lost!");
        }
    }


    #endregion
}
