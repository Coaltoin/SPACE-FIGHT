using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject enemy1, enemy2, enemy3;

    private float timeTilNextSpawn;
    private float spawnCD = 5;

    public Vector2 secondsBetweenSpawnsMinMax;

    public Vector3 spawnPosition;

    float nextSpawnTime;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        KillBasedEnemySpawner();
        timeTilNextSpawn += Time.deltaTime;
    }

    #region TimedEnemySpawner
    /*
    /// <summary>
    /// More Enemies Spawn Based on Time passed
    /// </summary>
    void TimedEnemySpawner()
    {
        if (Time.time > nextSpawnTime)
        {
            float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());

            nextSpawnTime = Time.time + secondsBetweenSpawns;

            Debug.Log(nextSpawnTime);

            float moveSpeed = Mathf.Lerp(1.5f, 3f, Difficulty.GetDifficultyPercent());
            SpawnRandomEnemies(1, 2, enemy1, moveSpeed);
        }
    }
    */
    #endregion

    /// <summary>
    /// More Enemies Spawn Based on Kills earned by Player
    /// </summary>
    void KillBasedEnemySpawner()
    {
        int AmountNeededToWinLvl_1 = gameManager.GetAmountNeededToWin_Lvl_1();
        int AmountNeededToWinLvl_2 = gameManager.GetAmountNeededToWin_Lvl_2();

        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            int KillsPerPhase = AmountNeededToWinLvl_1 / 3;

            int KillsNeededToPass_Phase1 = KillsPerPhase;
            int KillsNeededToPass_Phase2 = KillsPerPhase * 2;

            int CurrentKills = gameManager.GetCurrentAmount();
            if (timeTilNextSpawn > spawnCD)
            {
                timeTilNextSpawn = 0;

                if (CurrentKills < KillsNeededToPass_Phase1)
                {
                    spawnCD = 5f;
                    SpawnRandomEnemies(2, 4, enemy1, 2f);
                    Debug.Log("LVL 1: Phase 1");
                }
                else if (CurrentKills >= KillsNeededToPass_Phase1 && CurrentKills < KillsNeededToPass_Phase2)
                {
                    spawnCD = 3f;
                    SpawnRandomEnemies(3, 6, enemy1, 2.5f);
                    Debug.Log("LVL 1: Phase 2");
                }
                else if (CurrentKills >= KillsNeededToPass_Phase2)
                {
                    spawnCD = 2f;
                    SpawnRandomEnemies(5, 8, enemy1, 3f);
                    Debug.Log("LVL 1: Phase 3");
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            int KillsPerPhase = AmountNeededToWinLvl_2 / 3;

            int KillsNeededToPass_Phase1 = KillsPerPhase;
            int KillsNeededToPass_Phase2 = KillsPerPhase * 2;

            int CurrentKills = gameManager.GetCurrentAmount();
            if (timeTilNextSpawn > spawnCD)
            {
                timeTilNextSpawn = 0;

                if (CurrentKills < KillsNeededToPass_Phase1)
                {
                    spawnCD = 4f;
                    SpawnRandomEnemies(3, 4, enemy1, 2.75f);
                    Debug.Log("LVL 2: Phase 1");
                }
                else if (CurrentKills >= KillsNeededToPass_Phase1 && CurrentKills < KillsNeededToPass_Phase2)
                {
                    spawnCD = 3f;
                    SpawnRandomEnemies(1, 3, enemy1, 1.5f);
                    SpawnRandomEnemies(2, 5, enemy2, 3f);
                    Debug.Log("LVL 2: Phase 2");
                }
                else if (CurrentKills >= KillsNeededToPass_Phase2)
                {
                    spawnCD = 2f;
                    SpawnRandomEnemies(1, 4, enemy1, 2f);
                    SpawnRandomEnemies(4, 5, enemy2, 3.7f);
                    Debug.Log("LVL 2: Phase 3");
                }
            }
        }
    }


    void SpawnRandomEnemies(int RandomMin, int RandomMax, GameObject Enemy, float EnemySpeed)
    {
        int numEnemiesSpawned = Random.Range(RandomMin, RandomMax);
        for (int i = 0; i < numEnemiesSpawned; i++)
        {
            float xPos, yPos;

            int whichEdge = Random.Range(1, 3);
            if (whichEdge == 1)
            {
                xPos = Random.Range(-11.5f, 11.5f);
                yPos = Random.Range(0, 2);
                if (yPos == 0) { yPos = 5.5f; }
                else if (yPos == 1) { yPos = -5.5f; }
            }
            else
            {
                yPos = Random.Range(-5.5f, 5.5f);
                xPos = Random.Range(0, 2);
                if (xPos == 0) { xPos = -11.5f; }
                else if (xPos == 1) { xPos = 11.5f; }
            }
            spawnPosition = new Vector3(xPos, yPos, 0);

            GameObject TheEnemy = Instantiate(Enemy, spawnPosition, Quaternion.identity);
            TheEnemy.GetComponent<EnemyController>().moveSpeed = EnemySpeed;
        }
    }
}