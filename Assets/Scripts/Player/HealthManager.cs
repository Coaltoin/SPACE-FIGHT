using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI HealthTextUI;
    private int health;
    private GameObject gameManager;

    private PlayerMovement player;

    void Start()
    {
        player = GameObject.Find("Player!").GetComponent<PlayerMovement>();
        gameManager = GameObject.Find("GameManager");
        health = 70;
        UpdateHealthTextUI();
    }

    private void Update()
    {
        UpdateHealthTextUI();
    }

    void UpdateHealthTextUI()
    {
        if (HealthTextUI)
        {
            HealthTextUI.text = "Health: " + health;
        }
    }

    public void TakeDamage(int amount)
    {
        if (player.killImpact == true) { return; } // Boosting and Immune to Damage

        health -= amount;

        if (health <= 0)
        {
            gameManager.GetComponent<GameManager>().DidPlayerBeatGame(false);
            Debug.Log("Player Died.");
        }
    }

    private bool CheckIfLose()
    {
        if (health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
