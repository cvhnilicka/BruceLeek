﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCurrentWave(int waveNum)
    {
        Text currentWave = gameObject.transform.Find("CurrentWave").GetComponent<Text>();
        currentWave.text = "Current Wave: " + waveNum;
    }

    public void UpdateScore(int newScore)
    {
        Text score = gameObject.transform.Find("Score").GetComponent<Text>();
        score.text = "Score: " + newScore;
    }
}
