using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class CarrotBullet : MonoBehaviour
{

    public GameObject player;
    float fireVelocity = 5f;

    Rigidbody2D carrotBody;

    // Start is called before the first frame update
    void Start()
    {
        carrotBody = GetComponent<Rigidbody2D>();
        float controlThrow = Mathf.Sign(CrossPlatformInputManager.GetAxis("Mouse X"));
        float playerDir = GameObject.FindGameObjectWithTag("Player").transform.localScale.x;
        carrotBody.velocity = new Vector2(fireVelocity*playerDir, 0f);

    }

 
}
