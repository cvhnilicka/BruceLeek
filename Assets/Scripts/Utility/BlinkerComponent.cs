using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkerComponent : MonoBehaviour
{
    SpriteRenderer myRenderer;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myRenderer.enabled = false;
        myAnimator.enabled = false;
    }

    public void SetHarvestable()
    {
        EnableBlinker();
        myAnimator.SetBool("harvestable", true);
    }

    public void DisableHarvestable()
    {
        myAnimator.SetBool("harvestable", false);

    }

    public void SetPlantable()
    {
        EnableBlinker();
        myAnimator.SetBool("plantable", true);
    }

    public void DisablePlantable()
    {
        myAnimator.SetBool("plantable", false);

    }

    public void EnableBlinker()
    {
        myRenderer.enabled = true;
        myAnimator.enabled = true;
    }
    public void DisableBlinker()
    {
        myAnimator.SetBool("plantable", false);
        myAnimator.SetBool("harvestable", false);
        myRenderer.enabled = false;
        myAnimator.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
