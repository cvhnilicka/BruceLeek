using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableComponent : MonoBehaviour
{

    BoxCollider2D myHarvestCollider;
    // Start is called before the first frame update
    void Start()
    {
        myHarvestCollider = GetComponent<BoxCollider2D>();
        DisableHarvestable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHarvestable()
    {
        myHarvestCollider.enabled = true;
    }

    public void DisableHarvestable()
    {
        myHarvestCollider.enabled = false;

    }
}
