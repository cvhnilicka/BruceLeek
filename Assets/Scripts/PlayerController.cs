using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    // config
    [Header("Config")]
    [SerializeField] float runSpeed = 3.0f;
    [SerializeField] float jumpSpeed = 5.0f;

    [SerializeField] float leekDurability = 20f;
    [SerializeField] float carrotAmmo = 20f;


    public GameObject carrotBullet;

    float damageTime = 3f;
    float damageTimer;


    // state
    bool isAlive = true;
    bool overHarvestableCrop;
    bool grabCrops;


    // cached componenets
    [Header("Components")]
    Rigidbody2D myBody;
    Animator myAnimator;
    Collider2D myCollider;
    HealthBar healthBar;
    MeleeAttack leekAttack;
    MeterController orangeMeter;
    MeterController greenMeter;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        healthBar = GetComponentInChildren<HealthBar>();
        leekAttack = GetComponentInChildren<MeleeAttack>();
        orangeMeter = transform.Find("OrangeMeter").GetComponent<MeterController>();
        UpdateOrangeMeter(this.carrotAmmo);
        greenMeter = transform.Find("GreenMeter").GetComponent<MeterController>();
        UpdateGreenMeter(this.leekDurability);
        damageTimer = 99f;
        overHarvestableCrop = false;
        grabCrops = false;

    }

    private void UpdateGreenMeter(float greenAmount)
    {
        greenMeter.ResetTotal(greenAmount);
    }
    private void UpdateOrangeMeter(float orangeAmount)
    {
        orangeMeter.ResetTotal(orangeAmount);
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSprite();
        WalkAnimation();
        Jump();
        Attack();
        if (overHarvestableCrop) GrabCrops();

        damageTimer += Time.deltaTime;
        //JumpAnimation();
    }

    private void Attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            if (greenMeter.GetCurrentAmount() <= 0) return;
            myAnimator.SetTrigger("LeekAttack");
            leekAttack.AttackActivate();
            greenMeter.ReduceMeter(leekDurability * .2f);
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            if (orangeMeter.GetCurrentAmount() <= 0) return;
            myAnimator.SetTrigger("CarrotGun");
            Instantiate(carrotBullet, transform.localPosition, transform.localRotation);
            orangeMeter.ReduceMeter(carrotAmmo * .25f);
        }
    }

    private void GrabCrops()
    {
        if (CrossPlatformInputManager.GetButtonDown("Harvest"))
        {
            grabCrops = true;
            myAnimator.SetTrigger("Garden");
            //Instantiate(carrotBullet, transform.localPosition, transform.localRotation);
        }
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myBody.velocity.y);
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

    private void FlipSprite()
    {
        bool playerHasXVelocity = Mathf.Abs(myBody.velocity.x) > Mathf.Epsilon;
        if (playerHasXVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(myBody.velocity.x), 1f);
        }
    }

    private void JumpAnimation()
    {
        if (Mathf.Abs(myBody.velocity.y) > 0)
        {
            myAnimator.SetBool("Jumping", true);
        }
        else
        {
            myAnimator.SetBool("Jumping", false);
        }
    }

    private void WalkAnimation()
    {
        if (Mathf.Abs(myBody.velocity.x) > Mathf.Epsilon)
        {
            myAnimator.SetBool("Walking", true);
        }
        else
        {
            myAnimator.SetBool("Walking", false);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("weapon")))
        {
            if (damageTimer >= damageTime)
            {
                healthBar.LargeHit();
                damageTimer = 0;
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("crops")))
        {
            transform.GetComponentInChildren<EBlink>().EnableEblink();
            overHarvestableCrop = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("crops")))
        {
           
            if (grabCrops && overHarvestableCrop)
            {
                Debug.Log("collision gameobject name: " + collision.gameObject.name);
                // need to harvest crops here and then restart grow process
                collision.transform.GetComponent<CropController>().Harvest();
                if (collision.gameObject.name.Contains("CarrotCrop"))
                {
                    print("UPDATING ORANGE METER");
                    UpdateOrangeMeter(carrotAmmo);
                }
                else if (collision.gameObject.name.Contains("LeekCrop"))
                {
                    UpdateGreenMeter(leekDurability);
                }
                overHarvestableCrop = false;
                grabCrops = false;
                //collision.transform.GetComponentInChildren<CropController>().Harvest();
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
     
            

      
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.GetComponentInChildren<EBlink>().DisableEblink();

    }



}
