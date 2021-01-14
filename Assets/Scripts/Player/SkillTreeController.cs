﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeController : MonoBehaviour
{
    //readonly Dictionary<int, int> HeatlhTree = new Dictionary<int, int>({0:10,1:15});
    int[] HealthTree = new int[] { 10, 15, 20, 25 };
    private int currentHealthLevel;
    public readonly int MaxHealthLevel = 3;


    int[] GrowPatchesTree = new int[] { 1, 2, 3 };
    private int currentPatchLevel;
    public readonly int MaxPatches = 2;


    int[] WeaponDamageTree = new int[] { 1, 10, 20, 30 };
    private int currentWeaponDamageLevel;
    public readonly int MaxWeaponDamageLevel = 3;


    HealthBranch healthBranch;

    // Start is called before the first frame update
    void Start()
    {
       
        currentHealthLevel = 0;
        currentPatchLevel = 0;
        currentWeaponDamageLevel = 0;


        healthBranch = GetComponentInChildren<HealthBranch>();
    }

    void Update()
    {
        HealthBranchUpdate();
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

    public int GetHealthTreeAmount()
    {
        print("CUrrent Health Level: " + this.currentHealthLevel);
        //print(this.HeatlhTree[0]);
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

    public int GetWeaponDamage()
    {
        return this.WeaponDamageTree[currentWeaponDamageLevel];
    }
    
}
