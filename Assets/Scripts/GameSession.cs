using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameSession : MonoBehaviour
{

    [SerializeField] int MaxEnemies = 3;
    [SerializeField] float enemySpawnTime = 3f;
    [SerializeField] GameObject[] enemies;

    GameObject[] spawnPoints;
    ImageDigitAnimator[] timerImages;

    PlayerController player;

 

    Wave currentWave;

    [SerializeField] GameObject waveBackground;



    float breakTimer;
    [SerializeField] float breakTime = 10f;

    float gameTimer = 120f;

    int waveNum;


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
        waveNum = 1;
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        GameObject timer = gameObject.transform.Find("TimerV1").gameObject;
        timerImages = timer.GetComponentsInChildren<ImageDigitAnimator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //Debug.Log("Timer Images Animators Length: " + timerImages.Length);
        currentWave = new Wave(waveNum, spawnPoints, enemies);
        TimerAnimators(gameTimer);
        waveBackground.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {
        WaveController();
        TimerAnimators(breakTimer);
        
    }

    private void WaveController()
    {
        // updating the current running wave
        if (currentWave != null)
        {
            if (!waveBackground.activeInHierarchy) waveBackground.SetActive(true);

            //Debug.Log("IN WAVE");

            currentWave.Update();


            if (currentWave.WaveComplete())
            {
                currentWave = null;
                breakTimer = breakTime;
            }

        }
        else if (breakTimer > 0f && currentWave == null)
        {
            if (waveBackground.activeInHierarchy) waveBackground.SetActive(false);
            //Debug.Log("Break TImer");
            breakTimer -= Time.deltaTime;
        }
        else
        {
            //Debug.Log("NEW WAVE");
            currentWave = new Wave(waveNum += 1, spawnPoints, enemies);
        }
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

}
