using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update

    private int health;
    private GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfLose())
        {
            gameManager.GetComponent<GameManager>().DidPlayerBeatGame(false);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
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
