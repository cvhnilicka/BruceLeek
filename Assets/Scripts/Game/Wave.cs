using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/*
 * 
 * This is to serve as the WAVE info.
 * 
 * A WAVE is to be instantiated when the PLAYER will be in the middle of an
 * attack by the enemies. 
 * 
 * should probably move enemy creation to here
 * 
 * **/

// Todo: NEED TO FIGURE OUT A MORE DYNAMIC SPAW RATE IN THE FUTURE

public class Wave
{
    float enemySpawnTime = 3f;

    private int waveNum;
    private GameObject[] spawnPoints;
    private GameObject[] enemies;
    int maxEnemies;
    int numEnemies;
    int activeEnemies;

    GameSession session;


    float enemySpawnTimer;

    public Wave(int waveNum, GameObject[] spawnPoints,
        GameObject[] enemies)
    {
        this.waveNum = waveNum;
        this.spawnPoints = spawnPoints;
        this.enemies = enemies;
        //this.session = session;
        enemySpawnTimer = 0f;
        numEnemies = 0;
        maxEnemies = DetermineNumberOfEnemies(this.waveNum);
        Debug.Log("Wave Num: " + waveNum + "\n:NumEnemies: " + maxEnemies);
    }


    public void Update()
    {
        activeEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemySpawnTimer += Time.deltaTime;
        SpawnEnemies();
    }


    private Transform RandomSpawn()
    {
        int numSpawnPoints = spawnPoints.Length;
        return spawnPoints[Random.Range(0, numSpawnPoints)].transform;
    }

    private GameObject RandomEnemy()
    {
        int numEnemies = enemies.Length;

        return this.enemies[Random.Range(0, numEnemies-1)];
    }

    public bool WaveComplete()
    {
        if (numEnemies == maxEnemies && activeEnemies == 0) return true;
        else return false;
    }


    private void SpawnEnemies()
    {

        if (enemySpawnTimer >= enemySpawnTime)
        {

            if (numEnemies < maxEnemies)
            {

                Vector3 spawPos = RandomSpawn().localPosition;
                Object.Instantiate(RandomEnemy(), spawPos, Quaternion.identity);
                numEnemies += 1;
                enemySpawnTimer = 0f;
            }
        }
    }



    /*
     * 
     * 
     * Somehow need to figure out how to dynamically determine how many enemies
     * based off of waveNum
     * 
     * 
     * **/

    private int DetermineNumberOfEnemies(int waveNum)
    {
        int ret = 1;
        if (waveNum > 0 && waveNum <= 2)
        {
            ret = 2;
        }
        else if (waveNum > 2 && waveNum <= 5)
        {
            ret = 5;
        }
        else if (waveNum > 5 && waveNum <= 10)
        {
            ret = 8;
        }
        else if (waveNum > 10 && waveNum <= 15)
        {
            ret = 12;
        }
        else if (waveNum > 15 && waveNum <= 20)
        {
            ret = 15;
        }
        else if (waveNum > 25)
        {
            ret = 20;
        }

        return ret;
    }


}
