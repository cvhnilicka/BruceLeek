using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowScript : MonoBehaviour
{

    Vector3 diff;
    Transform playerObj;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = transform.parent.GetComponentInChildren<MovementController>().transform;
        diff = playerObj.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObj.position-diff;
    }
}
