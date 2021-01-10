using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{

    [SerializeField] float growTime = 15f;

    Animator myAnimator;
    BoxCollider2D myHarvestCollider;


    float growTimer;
    private bool grow;
    private bool harvest;
    // Start is called before the first frame update
    void Start()
    {
        growTimer = 0f;
        myAnimator = GetComponent<Animator>();
        myHarvestCollider = GetComponent<BoxCollider2D>();
        myHarvestCollider.enabled = false;
        grow = false;
        harvest = false;
        StartGrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (grow) Grow();
        if (harvest) Harvestable();
        if (growTimer < 0)
        {
            myHarvestCollider.enabled = false;
        }

    }

    public void StartGrow()
    {
        grow = true;
        growTimer = 0f;

    }

    private void Harvestable()
    {
        myHarvestCollider.enabled = true;
    }

    public void Harvest()
    {
        growTimer = -1f;
        float perc = growTimer / growTime;
        myAnimator.SetFloat("Percentage", perc);
        //myHarvestCollider.enabled = false;
        grow = false;
        harvest = false;
    }

    private void Grow()
    {
        growTimer += Time.deltaTime;
        float perc = growTimer / growTime;
        myAnimator.SetFloat("Percentage", perc);
        if (perc > 1.0)
        {
            grow = false;
            harvest = true;
        }
    }
}
