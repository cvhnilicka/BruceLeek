using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MovementController : MonoBehaviour
{

    // config
    [Header("Config")]
    [SerializeField] float runSpeed = 10.0f;
    [SerializeField] float jumpSpeed = 24.0f;

    // cached componenets
    [Header("Components")]
    Rigidbody2D myBody;
    Collider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Jump();
    }


    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myBody.velocity.y);
        //print(playerVelocity);
        myBody.velocity = playerVelocity;
    }

    private void Jump()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("foreground")))
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
                myBody.velocity += jumpVelocity;

            }
        }

    }


}
