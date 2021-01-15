using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class AbilityController : MonoBehaviour
{
    [SerializeField] float leekDurability = 20f;
    [SerializeField] float carrotAmmo = 20f;
    public GameObject carrotBullet;


    bool grabCrops;
    bool plant;
    bool overHarvestableCrop;
    bool overPlantablePatch;
    float damageTime = 3f;
    float damageTimer;

    [Header("Components")]
    Rigidbody2D myBody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    MeleeAttack leekAttack;
    PlayerController parent;

    // Start is called before the first frame update
    void Start()
    {
        overHarvestableCrop = false;
        overPlantablePatch = false;
        grabCrops = false;
        plant = false;
        parent = GetComponentInParent<PlayerController>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
        myBody = GetComponent<Rigidbody2D>();
        leekAttack = GetComponentInChildren<MeleeAttack>();

    }

  

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        WalkAnimation();

        Attack();
        CropController();
        damageTimer += Time.deltaTime;
    }


    private void CropController()
    {
        if (overHarvestableCrop) GrabCrops();
        if (overPlantablePatch) PlantCrops();
    }


    public float GetCarrotAmmo()
    {
        return this.carrotAmmo;
    }

    public float GetLeekDurability()
    {
        return this.leekDurability;
    }



    private void FlipSprite()
    {
        bool playerHasXVelocity = Mathf.Abs(myBody.velocity.x) > Mathf.Epsilon;
        if (playerHasXVelocity)
        {
            //Debug.Log("X Vel: " + Mathf.Abs(myBody.velocity.x));
            transform.localScale = new Vector2(Mathf.Sign(myBody.velocity.x), 1f);
        }
    }


    private void WalkAnimation()
    {
        if (Mathf.Abs(myBody.velocity.x) > 0.01f)
        {
            print("myBody.velocity.x: " + myBody.velocity.x);
            myAnimator.SetBool("Walking", true);
        }
        else
        {
            if (myAnimator.GetBool("Walking")) myAnimator.SetBool("Walking", false);

        }
    }

    private void Attack()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            if (parent.GetGreenMeter().GetCurrentAmount() <= 0) return;
            myAnimator.SetTrigger("LeekAttack");
            leekAttack.AttackActivate();
            parent.GetGreenMeter().ReduceMeter(leekDurability * .2f);
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            if (parent.GetOrangeMeter().GetCurrentAmount() <= 0) return;
            myAnimator.SetTrigger("CarrotGun");
            Instantiate(carrotBullet, transform.localPosition, transform.localRotation);
            parent.GetOrangeMeter().ReduceMeter(carrotAmmo * .25f);
        }
    }

    private void GrabCrops()
    {
        if (CrossPlatformInputManager.GetButtonDown("Harvest"))
        {
            grabCrops = true;
            myAnimator.SetTrigger("Garden");
        }
    }

    private void PlantCrops()
    {
        if (CrossPlatformInputManager.GetButtonDown("Plant"))
        {
            plant = true;
            myAnimator.SetTrigger("Garden");
        }
    }



    /*
     * Ability Collision Handlers
     * 
     * **/


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("enemyBullet")))
        {
            if (damageTimer >= damageTime)
            {
                parent.TakeDamage(2f);
                damageTimer = 0;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("enemyBullet")))
        {
            if (damageTimer >= damageTime)
            {
                parent.TakeDamage(2f);
                damageTimer = 0;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        // handle blinkers upon entering the trigger collisions
        BlinkerController(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HarvestCollisionHandler(collision);
        PlanterCollisionHandler(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // disabled blinkers when leaving collisions (triggers)
        transform.parent.transform.GetComponentInChildren<BlinkerComponent>().DisableBlinker();
        overPlantablePatch = false;
        overHarvestableCrop = false;

    }

    private void BlinkerController(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("harvestable")))
        {
            transform.parent.GetComponentInChildren<BlinkerComponent>().SetHarvestable();
            overHarvestableCrop = true;
        }
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("plantable")))
        {
            transform.parent.GetComponentInChildren<BlinkerComponent>().SetPlantable();
            overPlantablePatch = true;
        }
    }

    private void PlanterCollisionHandler(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("plantable")))
        {
            // First make sure that we have enabled the plant action and that we are over a plantable patch
            if (plant && overPlantablePatch)
            {
                // start regrowing on the current patch we are hovered over
                collision.transform.parent.GetComponent<CropController>().StartGrow();
                // reset bools
                plant = false;
                overPlantablePatch = false;
            }
        }
    }


    private void HarvestCollisionHandler(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("harvestable")))
        {

            if (grabCrops && overHarvestableCrop)
            {
                // Here we can harvest the crop that we are hovering over
                collision.transform.GetComponentInParent<CropController>().Harvest();

                // harvest carrot
                if (collision.transform.parent.name.Contains("CarrotCrop"))
                {
                    parent.UpdateOrangeMeter(carrotAmmo);
                }
                // harvest leek
                else if (collision.transform.parent.name.Contains("LeekCrop"))
                {
                    parent.UpdateGreenMeter(leekDurability);
                }
                // reset bools used by actions/animations
                overHarvestableCrop = false;
                grabCrops = false;
            }
        }
    }
}
