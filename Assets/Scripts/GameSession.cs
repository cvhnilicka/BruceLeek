using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] int MaxEnemies = 3;
    [SerializeField] float enemySpawnTime = 3f;

    public GameObject BurgerMan;
    public GameObject SpawnPoint;

    GameObject[] spawnPoints;
    ImageDigitAnimator[] timerImages;


    float enemySpawnTimer;
    int numEnemies;

    float breakTimer;
    float breakTime = 30f;

    float gameTimer = 120f;


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
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject timer = gameObject.transform.Find("TimerV1").gameObject;
        timerImages = timer.GetComponentsInChildren<ImageDigitAnimator>();
        //Debug.Log("Timer Images Animators Length: " + timerImages.Length);
        TimerAnimators(gameTimer);

    }


    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        SpawnEnemies();
    }

    private void UpdateTimers()
    {
        gameTimer -= Time.deltaTime;
        enemySpawnTimer += Time.deltaTime;

        TimerAnimators(gameTimer);


    }

    private void TimerAnimators(float time)
    {
        int intTime = Mathf.FloorToInt(time);
        if (timerImages.Length == 3)
        {
            //print("intTIme: " + intTime);
            for (int i = 2; i >= 0; i--)
            {
                //timerImages[i].SetInteger()
                int digt = intTime % 10;
                intTime = intTime / 10;
                timerImages[i].SetImage(digt);
                //Debug.Log("Timer image [" + i + "] with digit: " + digt);

            }

        }
    }

    private Transform RandomSpawn()
    {
        int numSpawnPoints = spawnPoints.Length;
        return spawnPoints[Random.Range(0, numSpawnPoints)].transform;
    }

    private void SpawnEnemies()
    {
        if (enemySpawnTimer >= enemySpawnTime)
        {
            if (numEnemies < MaxEnemies)
            {
                Vector3 spawPos = RandomSpawn().localPosition;
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
