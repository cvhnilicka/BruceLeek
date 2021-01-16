using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantableComponent : MonoBehaviour
{
    BoxCollider2D myPlantableCollider;
    // Start is called before the first frame update
    void Start()
    {
        DisablePlantable();
    }

    private void Awake()
    {
        // i need this as they are being put inactive
        myPlantableCollider = GetComponent<BoxCollider2D>();

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
