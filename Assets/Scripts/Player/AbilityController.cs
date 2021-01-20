using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class AbilityController : MonoBehaviour
{
    [SerializeField] float leekDurability = 20f;
    [SerializeField] float carrotAmmo = 20f;
    [SerializeField] float leekEfficiency = 2f;
    [SerializeField] float carrotEfficiency = 1f;
    public GameObject carrotBullet;


    bool grabCrops;
    bool plant;
    bool overHarvestableCrop;
    bool overPlantablePatch;
    float damageTime = 3f;
    float damageTimer;
    bool shouldAttack;

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
        shouldAttack = true;
        damageTimer = 0f;
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

        if (parent.GetInWave()) Attack();
        CropController();
        damageTimer += Time.deltaTime;
    }

    public void SetShouldAttack(bool shouldAttack)
    {
        this.shouldAttack = shouldAttack;
    }

    public void UpgradeWeaponDamage()
    {
        leekAttack.GetComponent<IsWeapon>().SetWeaponDamage(parent.GetSkillTree().GetWeaponDamage() * leekAttack.GetComponent<IsWeapon>().GetWeaponDamage());
    }

    public void UpgradeToLongLeek()
    {
        BoxCollider2D leekCollider = leekAttack.GetComponent<BoxCollider2D>();
        leekCollider.size = new Vector2(leekCollider.size.x*2, leekCollider.size.y);
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

            myAnimator.SetBool("Walking", true);
        }
        else
        {
            if (myAnimator.GetBool("Walking")) myAnimator.SetBool("Walking", false);

        }
    }

    private void LeekAttackController()
    {
        if (parent.GetGreenMeter().GetCurrentAmount() <= 0) return;



        // Todo: Differentiate between Long and Short leek attack
        SkillTreeController.LeekType leekType = parent.GetSkillTree().GetCurrentLeekUnlock();

        switch (leekType)
        {
            case SkillTreeController.LeekType.SHORT:
                myAnimator.SetTrigger("LeekAttack");

                break;
            case SkillTreeController.LeekType.LONG:
                myAnimator.SetTrigger("LongLeek");

                break;
            case SkillTreeController.LeekType.KNOCKBACK:
                // Todo: call knockback
                break;
        }
        leekAttack.AttackActivate();
        parent.GetGreenMeter().ReduceMeter(leekDurability * leekEfficiency);
    }



    private void Attack()
    {
        // Todo: So here i will need to set the  weapon damage based on skill tree multiplier and base damage

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            LeekAttackController();
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire2"))
        {
            if (parent.GetOrangeMeter().GetCurrentAmount() <= 0) return;
            myAnimator.SetTrigger("CarrotGun");
            GameObject newBullet = Instantiate(carrotBullet, transform.localPosition, transform.localRotation);

            newBullet.GetComponent<IsWeapon>().SetWeaponDamage(newBullet.GetComponent<IsWeapon>().GetWeaponDamage() * parent.GetSkillTree().GetWeaponDamage());
            parent.GetOrangeMeter().ReduceMeter(carrotAmmo * carrotEfficiency);
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


    void Die()
    {
        myAnimator.SetTrigger("Death");
        parent.SetIsAlive(false);
        myBody.Sleep();
    }

    /*
     * Ability Collision Handlers
     * 
     * **/


    //private void take


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // handle blinkers upon entering the trigger collisions
        BlinkerController(collision);
        DamageCollisionHandler(collision);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HarvestCollisionHandler(collision);
        PlanterCollisionHandler(collision);
        //DamageCollisionHandler(collision);
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
    private void DamageCollisionHandler(Collider2D collision)
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("enemyBullet")))
        {
            if (damageTimer >= damageTime)
            {
                //int health
                //print(collision.gameObject.name);
                // here i need to grab the weapon damage from the enemy and damage the player with it
                if (collision.gameObject.tag == "Weapon")
                {
                     int health = parent.TakeDamage(collision.gameObject.GetComponent<IsWeapon>().GetWeaponDamage());
                    if (health <= 0)
                    {
                        Die();
                    }

                }
                // Todo: can probably clean and consolidate these
                else if (collision.gameObject.tag == "Enemy")
                {
                    int health = parent.TakeDamage(collision.gameObject.GetComponentInChildren<IsWeapon>().GetWeaponDamage());
                    if (health <= 0)
                    {
                        Die();
                    }

                }
                //if (health )
                damageTimer = 0;
            }
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
