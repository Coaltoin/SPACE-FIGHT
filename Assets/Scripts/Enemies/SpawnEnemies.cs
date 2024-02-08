using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    public GameObject enemy1, enemy2, enemy3;

    public float timeTilNextSpawn, spawnCD;

    public Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTilNextSpawn += Time.deltaTime;

        if (timeTilNextSpawn > spawnCD)
        {
            timeTilNextSpawn = 0;
            //spawna da enemies.
            int numEnemiesSpawned = Random.Range(3, 6);
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
                spawnPosition = new Vector3 (xPos, yPos, 0);
                Instantiate(enemy1, spawnPosition, Quaternion.identity);
            }
        }    
    }
}
