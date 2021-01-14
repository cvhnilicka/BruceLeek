using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // state
    bool isAlive = true;



    // cached componenets
    [Header("Components")]
    HealthBar healthBar;
    SkillTreeController skillTree;
    public MeterController orangeMeter;
    public MeterController greenMeter;
    AbilityController abilities;

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


    }

    public void UpdateGreenMeter(float greenAmount)
    {
        greenMeter.ResetTotal(greenAmount);
    }
    public void UpdateOrangeMeter(float orangeAmount)
    {
        orangeMeter.ResetTotal(orangeAmount);
    }

    public void TakeDamage(float amount)
    {
        healthBar.LargeHit();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void Upgrade()
    {
        skillTree.IncreaseHealthTree();
        healthBar.SetHealth(skillTree.GetHealthTreeAmount());
    }

    

}
