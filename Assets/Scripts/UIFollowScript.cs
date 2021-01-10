using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowScript : MonoBehaviour
{

    Vector3 diff;
    PlayerController playerObj;
    // Start is called before the first frame update
    void Start()
    {
        playerObj = transform.parent.GetComponentInChildren<PlayerController>();
        diff = playerObj.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerObj.transform.position-diff;
    }
}
