using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{

    [SerializeField] float growTime = 15f;

    Animator myAnimator;
    HarvestableComponent myHarvestableComponent;
    PieTimer myPieTimer;


    float growTimer;
    private bool grow;
    private bool harvest;
    // Start is called before the first frame update
    void Start()
    {
        growTimer = 0f;
        myAnimator = GetComponent<Animator>();
        myPieTimer = GetComponentInChildren<PieTimer>();
        myHarvestableComponent = GetComponentInChildren<HarvestableComponent>();
        grow = false;
        harvest = false;
        StartGrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (grow) Grow();
        if (harvest) myHarvestableComponent.EnableHarvestable();
        if (growTimer < 0)
        {
            myHarvestableComponent.DisableHarvestable();
        }

    }

    public void StartGrow()
    {
        grow = true;
        growTimer = 0f;

    }

    public void Harvest()
    {
        growTimer = -1f;
        float perc = growTimer / growTime;
        myAnimator.SetFloat("Percentage", perc);
        myPieTimer.SetTimerPercentage(perc);
        grow = false;
        harvest = false;
    }

    private void Grow()
    {
        growTimer += Time.deltaTime;
        float perc = growTimer / growTime;
        myAnimator.SetFloat("Percentage", perc);
        myPieTimer.SetTimerPercentage(perc);

        if (perc > 1.0)
        {
            grow = false;
            harvest = true;
        }
    }
}
