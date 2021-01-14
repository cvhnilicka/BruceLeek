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
    private MeterController orangeMeter;
    private MeterController greenMeter;
    AbilityController abilities;

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


    }
    public void SetInWave(bool inWave)
    {
        this.inWave = inWave;
    }

    public bool GetInWave()
    {
        return this.inWave;

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

        SkillTree();
    }

    public void SkillTree()
    {
        skillTree.Display(!inWave);
    }

    public void Upgrade()
    {
        skillTree.IncreaseHealthTree();
        healthBar.SetHealth(skillTree.GetHealthTreeAmount());
    }

    public MeterController GetGreenMeter()
    {
        return this.greenMeter;
    }

    public MeterController GetOrangeMeter()
    {
        return this.orangeMeter;
    }

    

}
