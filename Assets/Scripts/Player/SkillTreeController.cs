using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeController : MonoBehaviour
{
    int[] HealthTree = new int[] { 10, 15, 20, 25 };
    private int currentHealthLevel;
    public readonly int MaxHealthLevel = 3;


    int[] GrowPatchesTree = new int[] { 1, 2, 3 };
    private int currentPatchLevel;
    public readonly int MaxPatches = 2;


    float[] WeaponDamageTree = new float[] { 1, 1.1f, 1.25f };
    private int currentWeaponDamageLevel;
    public readonly int MaxWeaponDamageLevel = 2;

    public enum LeekType { SHORT, LONG, KNOCKBACK };
    LeekType[] LeekUpgradeTree = new LeekType[] { LeekType.SHORT, LeekType.LONG, LeekType.KNOCKBACK };
    private LeekType currentLeekType;
    private int currentLeekUpgrade;
    public readonly int MaxLeekUpgrades = 1;


    bool LongLeekUnlocked = false;


    HealthBranch healthBranch;
    PatchBranch patchBranch;
    WeaponDamageBranch weapondDamageBranch;
    LeekUpgradeBranch leekUpgradeBranch;
    Animator myAnimator;
    GameObject labels;

    // Start is called before the first frame update
    void Start()
    {
       
        currentHealthLevel = 0;
        currentPatchLevel = 0;
        currentWeaponDamageLevel = 0;
        currentLeekUpgrade = 0;

        myAnimator = GetComponent<Animator>();
        healthBranch = GetComponentInChildren<HealthBranch>();
        patchBranch = GetComponentInChildren<PatchBranch>();
        weapondDamageBranch = GetComponentInChildren<WeaponDamageBranch>();
        leekUpgradeBranch = GetComponentInChildren<LeekUpgradeBranch>();

        labels = transform.Find("Labels").gameObject;
    }

    void Update()
    {
        HealthBranchUpdate();
        PatchesBranchUpdate();
        WeaponDamageBranchUpdate();
        LeekBranchUpdate();
    }


    /*
     * 
     * Update Functions for branches
     * 
     * **/

    void PatchesBranchUpdate()
    {
        for (int i = 0; i <= currentPatchLevel; i++)
        {
            SkillTreeLeafController leaf = patchBranch.transform.
                Find("PatchNode" + GetNumPatches().ToString()).GetComponentInChildren<SkillTreeLeafController>();
            leaf.SelectedSprite();
        }
        if (currentPatchLevel < MaxPatches)
        {
            patchBranch.transform.
                Find("PatchNode" + this.GrowPatchesTree[currentPatchLevel+1].ToString())
                .GetComponentInChildren<SkillTreeLeafController>().EnableHoverCollider();
        }
    }



    void HealthBranchUpdate()
    {
        for(int i = 0; i <= currentHealthLevel; i++)
        {
            SkillTreeLeafController leaf = healthBranch.transform.
                Find("HealthNode" + GetHealthTreeAmount().ToString()).GetComponent<SkillTreeLeafController>();

            leaf.SelectedSprite();

        }
        if (currentHealthLevel < MaxHealthLevel)
        {
            healthBranch.transform.Find("HealthNode" + this.HealthTree[currentHealthLevel + 1]
                .ToString()).GetComponent<SkillTreeLeafController>().EnableHoverCollider();
        }
    }

    void WeaponDamageBranchUpdate()
    {
        for (int i = 0; i <= currentWeaponDamageLevel; i++)
        {
            SkillTreeLeafController leaf;
            switch (i)
            {
                case 0:
                    leaf = weapondDamageBranch.transform.Find("WeaponDamageNode1").GetComponent<SkillTreeLeafController>();
                    break;
                case 1:
                    leaf = weapondDamageBranch.transform.Find("WeaponDamageNode10").GetComponent<SkillTreeLeafController>();
                    break;
                case 2: leaf = weapondDamageBranch.transform.Find("WeaponDamageNode25").GetComponent<SkillTreeLeafController>();
                    break;
                default: leaf = weapondDamageBranch.transform.Find("WeaponDamageNode1").GetComponent<SkillTreeLeafController>();
                    break;


            }

            leaf.SelectedSprite();
        }
        if (currentWeaponDamageLevel < MaxWeaponDamageLevel)
        {
            switch (currentWeaponDamageLevel)
            {
    
                case 0:
                    weapondDamageBranch.transform.Find("WeaponDamageNode10").GetComponent<SkillTreeLeafController>().EnableHoverCollider();
                    break;
                case 1:
                    weapondDamageBranch.transform.Find("WeaponDamageNode25").GetComponent<SkillTreeLeafController>().EnableHoverCollider();
                    break;
                default:
                    break;
            }

        }
    }

    void LeekBranchUpdate()
    {
        for (int i = 0; i <= currentLeekUpgrade; i++)
        {
            SkillTreeLeafController leaf;
            switch (i)
            {
                case 0:
                    leaf = leekUpgradeBranch.transform.Find("LeekUpgradeShort").GetComponent<SkillTreeLeafController>();
                    break;
                case 1:
                    leaf = leekUpgradeBranch.transform.Find("LeekUpgradeLong").GetComponent<SkillTreeLeafController>();
                    break;
                case 2:
                    leaf = leekUpgradeBranch.transform.Find("LeekUpgradeKnockback").GetComponent<SkillTreeLeafController>();
                    break;
                default:
                    leaf = leekUpgradeBranch.transform.Find("LeekUpgradeShort").GetComponent<SkillTreeLeafController>();
                    break;


            }

            leaf.SelectedSprite();

        }
        if (currentLeekUpgrade < MaxLeekUpgrades)
        {
            switch (currentLeekUpgrade)
            {

                case 0:
                    leekUpgradeBranch.transform.Find("LeekUpgradeLong").GetComponent<SkillTreeLeafController>().EnableHoverCollider();
                    break;
                case 1:
                    leekUpgradeBranch.transform.Find("LeekUpgradeKnockback").GetComponent<SkillTreeLeafController>().EnableHoverCollider();
                    break;
                default:
                    break;
            }

        }
    }



    /*
     * 
     * Get and Set Fields
     * 
     * **/

    //public LeekType GetCurrentLeekType()
    //{
    //    return this.currentLeekType;
    //}
    //public

    public LeekType GetCurrentLeekUnlock()
    {
        return this.LeekUpgradeTree[currentLeekUpgrade];
    }

    public bool IncreaseLeekUpgrades()
    {
        if (currentLeekUpgrade < MaxLeekUpgrades)
        {
            currentLeekUpgrade += 1;
            return true;
        }
        return false;
    }

    public int GetHealthTreeAmount()
    {
        return this.HealthTree[currentHealthLevel];
    }

    public bool IncreaseHealthTree()
    {
        if (currentHealthLevel < MaxHealthLevel)
        {
            currentHealthLevel += 1;
            return true;
        }
        return false;
    }

    public bool IncreasePatchesTree()
    {
        if (currentPatchLevel < MaxPatches)
        {
            currentPatchLevel += 1;
            return true;
        }
        return false;
    }

    public int GetNumPatches()
    {
        return this.GrowPatchesTree[currentPatchLevel];
    }

    public bool IncreaseWeaponDamageLevel()
    {
        if (currentWeaponDamageLevel < MaxWeaponDamageLevel)
        {
            currentWeaponDamageLevel += 1;
            return true;
        }
        return false;
    }

    public float GetWeaponDamage()
    {
        return this.WeaponDamageTree[currentWeaponDamageLevel];
    }



    /*
     * 
     * Display
     * 
     * **/
    public void Display(bool display)
    {
        if (healthBranch.gameObject.activeInHierarchy != display) healthBranch.gameObject.SetActive(display);
        if (patchBranch.gameObject.activeInHierarchy != display) patchBranch.gameObject.SetActive(display);
        if (weapondDamageBranch.gameObject.activeInHierarchy != display) weapondDamageBranch.gameObject.SetActive(display);
        if (leekUpgradeBranch.gameObject.activeInHierarchy != display) leekUpgradeBranch.gameObject.SetActive(display);
        if (labels.gameObject.activeInHierarchy != display) labels.gameObject.SetActive(display);
        if (myAnimator.GetBool("Visible") != display) myAnimator.SetBool("Visible", display);

    }

}
