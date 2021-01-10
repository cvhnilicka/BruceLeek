using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] int MaxEnemies = 3;
    [SerializeField] float enemySpawnTime = 3f;

    public GameObject BurgerMan;
    public GameObject SpawnPoint;


    float enemySpawnTimer;
    int numEnemies;

    float breakTimer;
    float breakTime = 30f;


    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnTimer = 0f;
        numEnemies = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        enemySpawnTimer += Time.deltaTime;
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if (enemySpawnTimer >= enemySpawnTime)
        {
            if (numEnemies < MaxEnemies)
            {
                Vector3 spawPos = SpawnPoint.transform.localPosition;
                Instantiate(BurgerMan, spawPos, transform.rotation);
                AddEnemy();
                enemySpawnTimer = 0f;
            }
        }
    }
    public void AddEnemy()
    {
        numEnemies += 1;
    }    
}
