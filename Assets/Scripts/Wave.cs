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

public class Wave
{
    float enemySpawnTime = 3f;

    private int waveNum;
    private GameObject[] spawnPoints;
    private GameObject[] waveEnemies;
    int maxEnemies;
    int numEnemies;



    float enemySpawnTimer;

    public Wave(int waveNum, GameObject[] spawnPoints,
        GameObject[] enemies)
    {
        this.waveNum = waveNum;
        this.spawnPoints = spawnPoints;
        this.waveEnemies = enemies;
        enemySpawnTimer = 0f;
        numEnemies = 0;
        maxEnemies = 3;
    }


    public void Update()
    {
        enemySpawnTimer += Time.deltaTime;
        SpawnEnemies();
        //Debug.Log("Wave Remaining Duration: " + this.duration);
    }


    private Transform RandomSpawn()
    {
        int numSpawnPoints = spawnPoints.Length;
        return spawnPoints[Random.Range(0, numSpawnPoints)].transform;
    }

    private GameObject RandomEnemy()
    {
        int numEnemies = waveEnemies.Length;

        return this.waveEnemies[Random.Range(0, numEnemies)];
    }

    public void AddEnemy()
    {
        numEnemies += 1;
    }


    private void SpawnEnemies()
    {
        Debug.Log("enemySpawnTimer: " + enemySpawnTimer);

        if (enemySpawnTimer >= enemySpawnTime)
        {

            if (numEnemies < maxEnemies)
            {
                Debug.Log("Spawning Enemies!");

                Vector3 spawPos = RandomSpawn().localPosition;
                GameObject.Instantiate(RandomEnemy(), spawPos, Quaternion.identity);
                AddEnemy();
                enemySpawnTimer = 0f;
            }
        }
    }
}
