using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // state
    bool isAlive = true;
    bool hasUpgrade = false;



    // cached componenets
    [Header("Components")]
    HealthBar healthBar;
    SkillTreeController skillTree;
    private MeterController orangeMeter;
    private MeterController greenMeter;
    AbilityController abilities;
    CropController[] allLeeks;
    CropController[] allCarrots;
    private int activeCarrot;
    private int activeLeek;




    bool inWave = false;

    // Start is called before the first frame update
    void Start()
    {
        skillTree = GetComponentInChildren<SkillTreeController>();
        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.SetHealth(skillTree.GetHealthTreeAmount());
        abilities = GetComponentInChildren<AbilityController>();

        orangeMeter = transform.Find("OrangeMeter").GetComponent<MeterController>();
        UpdateOrangeMeter(abilities.GetCarrotAmmo());
        greenMeter = transform.Find("GreenMeter").GetComponent<MeterController>();
        UpdateGreenMeter(abilities.GetLeekDurability());
        GetAllCrops();


    }
    // Update is called once per frame
    void Update()
    {
        SkillTree();
    }

    public void SetInWave(bool inWave)
    {
        if (this.inWave != inWave)
        {
            this.inWave = inWave;
        }
    }

    public bool GetInWave()
    {
        return this.inWave;

    }

    public void PlayerHasUpgrade()
    {
        hasUpgrade = true;
    }

    public void UpdateGreenMeter(float greenAmount)
    {
        greenMeter.ResetTotal(greenAmount);
    }
    public void UpdateOrangeMeter(float orangeAmount)
    {
        orangeMeter.ResetTotal(orangeAmount);
    }

    public int TakeDamage(float amount)
    {
        int health = healthBar.TakeDamage(amount);
        return health;

    }

    public bool GetIsAlive()
    {
        return this.isAlive;
    }

    public void SetIsAlive(bool isAlive)
    {
        this.isAlive = isAlive;
    }

    

    

    public void SkillTree()
    {
            skillTree.Display(!inWave && hasUpgrade);
    
    }

    public void UpgradeSkillTree(string leaf)
    {
        // can use if/else for now but might need to think of a better way to do this
        if (leaf.Contains("HealthNode"))
        {
            Debug.Log("Upgrading HealthSkill Tree");
            if (skillTree.IncreaseHealthTree())
            {
                healthBar.IncreaseHealthAmount(skillTree.GetHealthTreeAmount());
            }
        }
        else if (leaf.Contains("PatchNode"))
        {
            Debug.Log("Upgrading patchSkill Tree");
            if (skillTree.IncreasePatchesTree())
            {
                // here i would need to increase our patches limit etc
                UpgradePatchNum();
            }
        }
        else if (leaf.Contains("WeaponDamageNode"))
        {
            if (skillTree.IncreaseWeaponDamageLevel())
            {
                // here i would need to increase weapon damage amounts
                abilities.UpgradeWeaponDamage();
            }
        }
        else if (leaf.Contains("LeekUpgrade"))
        {
            // unlock long leek attack
            if (skillTree.IncreaseLeekUpgrades())
            {
                switch(skillTree.GetCurrentLeekUnlock())
                {
                    case SkillTreeController.LeekType.LONG:
                        // here i need to increase the size of the collider
                        abilities.UpgradeToLongLeek();
                        break;
                    case SkillTreeController.LeekType.KNOCKBACK:
                        // here would be some of the associated functionality for knockback

                        break;

                }
            }
            
        }
        hasUpgrade = false;
    }


  
    public MeterController GetGreenMeter()
    {
        return this.greenMeter;
    }

    public MeterController GetOrangeMeter()
    {
        return this.orangeMeter;
    }
    public SkillTreeController GetSkillTree()
    {
        return this.skillTree;
    }


    void UpgradePatchNum()
    {
        int num = skillTree.GetNumPatches();
        if (activeCarrot < num)
        {
            foreach (CropController carrot in allCarrots)
            {
                if (!carrot.gameObject.activeInHierarchy)
                {
                    carrot.gameObject.SetActive(true);
                    activeCarrot += 1;
                    break;
                }
            }
        }
        if (activeLeek < num)
        {
            foreach (CropController leek in allLeeks)
            {
                if (!leek.gameObject.activeInHierarchy)
                {
                    leek.gameObject.SetActive(true);
                    activeLeek += 1;
                    break;
                }
            }
        }
    }





    void GetAllCrops()
    {
        GameObject[] allCrops = GameObject.FindGameObjectsWithTag("Crop");
        allLeeks = new CropController[3];
        allCarrots = new CropController[3];
        int cIdx = 0
            , lIdx = 0;
        activeCarrot = 0;
        activeLeek = 0;

        foreach (GameObject crop in allCrops)
        {
            //print(skillTree.GetNumPatches());
            if (crop.name.Contains("Carrot"))
            {
                allCarrots[cIdx] = crop.GetComponent<CropController>();
                if (cIdx > 0) crop.SetActive(false);
                else activeCarrot += 1;

                cIdx += 1;
                


            }
            if (crop.name.Contains("Leek"))
            {
                allLeeks[lIdx] = crop.GetComponent<CropController>();
                if (lIdx > 0) crop.SetActive(false);
                else activeLeek += 1;
                lIdx += 1;
                
               

            }
        }
        //print("Total Number of Active leeks: " + activeLeek);
        //print("Total Number of Active carrots: " + activeCarrot);

    }


}
