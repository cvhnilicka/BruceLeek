using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieTimer : MonoBehaviour
{

    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();

    }

    public void SetTimerPercentage(float perc)
    {
        myAnimator.SetFloat("Percentage", perc);
    }
}
