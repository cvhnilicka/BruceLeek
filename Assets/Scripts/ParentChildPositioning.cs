using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentChildPositioning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController playerObj = transform.GetComponentInChildren<PlayerController>();
        transform.position = playerObj.transform.position-playerObj.transform.localPosition;
    }
}
