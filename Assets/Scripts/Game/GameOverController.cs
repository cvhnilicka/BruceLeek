using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{

    //Text endScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEndScore(int endScore)
    {
        Text endScoreText = transform.Find("EndScore").GetComponent<Text>();
        endScoreText.text = "Your Score: " + endScore;
    }
}
