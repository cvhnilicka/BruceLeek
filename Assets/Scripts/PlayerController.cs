using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{


    [SerializeField] float leekDurability = 20f;
    [SerializeField] float carrotAmmo = 20f;


    public GameObject carrotBullet;

    float damageTime = 3f;
    float damageTimer;


    // state
    bool isAlive = true;
    bool overHarvestableCrop;
    bool grabCrops;
    bool plant;
    bool overPlantablePatch;


    // cached componenets
    [Header("Components")]
    Rigidbody2D myBody;
    Animator myAnimator;
    Collider2D myCollider;
    HealthBar healthBar;
    SkillTreeController skillTree;
    MeleeAttack leekAttack;
    MeterController orangeMeter;
    MeterController greenMeter;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        skillTree = transform.parent.GetComponentInChildren<SkillTreeController>();
        healthBar = transform.parent.GetComponentInChildren<HealthBar>();
        healthBar.SetHealth(skillTree.GetHealthTreeAmount());
        leekAttack = GetComponentInChildren<MeleeAttack>();
        orangeMeter = transform.parent.transform.Find("OrangeMeter").GetComponent<MeterController>();
        UpdateOrangeMeter(this.carrotAmmo);
        greenMeter = transform.parent.transform.Find("GreenMeter").GetComponent<MeterController>();
        UpdateGreenMeter(this.leekDurability);
        damageTimer = 99f;
        overHarvestableCrop = false;
        overPlantablePatch = false;
        grabCrops = false;
        plant = false;

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
        FlipSprite();
        WalkAnimation();

        Attack();
        if (overHarvestableCrop) GrabCrops();
        if (overPlantablePatch) PlantCrops();

        damageTimer += Time.deltaTime;
        //JumpAnimation();
        Debug.Log("Total Health: " + healthBar.GetTotalHealth());
    }

    public void Upgrade()
    {
        skillTree.IncreaseHealthTree();
        healthBar.SetHealth(skillTree.GetHealthTreeAmount());
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
                    UpdateOrangeMeter(carrotAmmo);
                }
                else if (collision.transform.parent.name.Contains("LeekCrop"))
                {
                    UpdateGreenMeter(leekDurability);
                }
                overHarvestableCrop = false;
                grabCrops = false;
            }
        }
    }

}
