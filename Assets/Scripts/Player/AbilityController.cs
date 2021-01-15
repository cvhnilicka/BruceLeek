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
        if (overHarvestableCrop) GrabCrops();
        if (overPlantablePatch) PlantCrops();
        damageTimer += Time.deltaTime;
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
            transform.localScale = new Vector2(Mathf.Sign(myBody.velocity.x), 1f);
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
                Debug.Log("AbilityController:TakeDamage");   
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
                Debug.Log("AbilityController:TakeDamage");
                parent.TakeDamage(2f);
                damageTimer = 0;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        HarvestCollisionHandler(collision);
        PlanterCollisionHandler(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent.transform.GetComponentInChildren<BlinkerComponent>().DisableBlinker();
        overPlantablePatch = false;
        overHarvestableCrop = false;

    }

    private void PlanterCollisionHandler(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("plantable")))
        {

            if (plant && overPlantablePatch)
            {
                collision.transform.parent.GetComponent<CropController>().StartGrow();
                plant = false;
                overPlantablePatch = false;
                //collision.transform.GetComponentInParent<CropController>().Harvest();
            }
        }
    }


    private void HarvestCollisionHandler(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("harvestable")))
        {

            if (grabCrops && overHarvestableCrop)
            {
                //Debug.Log("collision gameobject name: " + collision.gameObject.name);
                // need to harvest crops here and then restart grow process
                collision.transform.GetComponentInParent<CropController>().Harvest();

                if (collision.transform.parent.name.Contains("CarrotCrop"))
                {
                    parent.UpdateOrangeMeter(carrotAmmo);
                }
                else if (collision.transform.parent.name.Contains("LeekCrop"))
                {
                    parent.UpdateGreenMeter(leekDurability);
                }
                overHarvestableCrop = false;
                grabCrops = false;
            }
        }
    }
}
