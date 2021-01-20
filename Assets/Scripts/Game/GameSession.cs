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
    UIController uiController;

 

    Wave currentWave;

    [SerializeField] GameObject waveBackground;



    float breakTimer;
    [SerializeField] float breakTime = 10f;

    float gameTimer = 120f;

    int waveNum;
    int score;

    bool upgraded = false;


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
        uiController = GetComponentInChildren<UIController>();
        timerImages = timer.GetComponentsInChildren<ImageDigitAnimator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        //Debug.Log("Timer Images Animators Length: " + timerImages.Length);
        currentWave = new Wave(waveNum, spawnPoints, enemies);
        TimerAnimators(gameTimer);
        waveBackground.SetActive(false);
        upgraded = false;
        score = 0;

    }


    // Update is called once per frame
    void Update()
    {
        WaveController();
        TimerAnimators(breakTimer);
        uiController.UpdateCurrentWave(waveNum);


    }

    //public UIController GetUIController()
    //{
    //    return this.uiController;
    //}

    public void UpdateUIScore(int addition)
    {
        this.uiController.UpdateScore(this.score += addition);
    }
   

    private void WaveController()
    {
        // updating the current running wave
        if (currentWave != null)
        {
            if (!waveBackground.activeInHierarchy) waveBackground.SetActive(true);

            currentWave.Update();
            player.SetInWave(true);

            if (currentWave.WaveComplete())
            {
                this.uiController.UpdateScore(this.score += 1000);
                currentWave = null;
                breakTimer = breakTime;
            }

        }
        else if (breakTimer > 0f && currentWave == null)
        {
            if (waveBackground.activeInHierarchy)
            {
                waveBackground.SetActive(false);
                player.PlayerHasUpgrade();

            }
            player.SetInWave(false);
            breakTimer -= Time.deltaTime;
        }
        else
        {
            currentWave = new Wave(waveNum += 1, spawnPoints, enemies);
        }
    }


    private void TimerAnimators(float time)
    {
        int intTime = Mathf.FloorToInt(time);
        if (timerImages.Length == 3)
        {
            for (int i = 2; i >= 0; i--)
            {
                int digt = intTime % 10;
                intTime = intTime / 10;
                timerImages[i].SetImage(digt);

            }

        }
    }

}
