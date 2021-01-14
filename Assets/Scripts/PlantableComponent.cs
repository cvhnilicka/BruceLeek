using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantableComponent : MonoBehaviour
{
    BoxCollider2D myPlantableCollider;
    // Start is called before the first frame update
    void Start()
    {
        myPlantableCollider = GetComponent<BoxCollider2D>();
        DisablePlantable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnablePlantable()
    {
        myPlantableCollider.enabled = true;
    }

    public void DisablePlantable()
    {
        myPlantableCollider.enabled = false;

    }
}
