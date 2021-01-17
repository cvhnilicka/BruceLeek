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


    HealthBranch healthBranch;
    PatchBranch patchBranch;
    WeaponDamageBranch weapondDamageBranch;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
       
        currentHealthLevel = 0;
        currentPatchLevel = 0;
        currentWeaponDamageLevel = 0;

        myAnimator = GetComponent<Animator>();
        healthBranch = GetComponentInChildren<HealthBranch>();
        patchBranch = GetComponentInChildren<PatchBranch>();
        weapondDamageBranch = GetComponentInChildren<WeaponDamageBranch>();
    }

    void Update()
    {
        HealthBranchUpdate();
        PatchesBranchUpdate();
        WeaponDamageBranchUpdate();
    }

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

            //weapondDamageBranch.transform
            //    .Find("WeaponDamageNode" + this.WeaponDamageTree[currentWeaponDamageLevel + 1]
            //    .ToString()).GetComponent<SkillTreeLeafController>().EnableHoverCollider();
        }
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

    public void Display(bool display)
    {
        if (healthBranch.gameObject.activeInHierarchy != display) healthBranch.gameObject.SetActive(display);
        if (patchBranch.gameObject.activeInHierarchy != display) patchBranch.gameObject.SetActive(display);
        if (weapondDamageBranch.gameObject.activeInHierarchy != display) weapondDamageBranch.gameObject.SetActive(display);
        if (myAnimator.GetBool("Visible") != display) myAnimator.SetBool("Visible", display);
    }

}
