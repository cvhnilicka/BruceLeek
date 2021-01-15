using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] int largeHitValue = 3;
    [SerializeField] int smallHitValue = 1;

    private int TotalHealth = 10;
    private int currentHealth;

    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        //TotalHealth = transform.parent.GetComponent<PlayerController>().MaxHealth;
        currentHealth = TotalHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //AnimationController();
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);

    }

    void RenderCheck()
    {
        if (currentHealth <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void SetHealth(int health)
    {
        this.TotalHealth = health;
    }
    public void IncreaseHealthAmount(int increase)
    {
        this.TotalHealth = increase;
        this.currentHealth += 5;
        AnimationController();
    }

    public int GetCurrentHealth()
    {
        return this.currentHealth;
    }

    public int GetTotalHealth()
    {
        return this.TotalHealth;
    }


    public int LargeHit()
    {
        currentHealth -= largeHitValue;
        AnimationController();
        RenderCheck();
        return currentHealth;

    }

    public int SmallHit()
    {
        currentHealth -= smallHitValue;
        AnimationController();
        RenderCheck();
        return currentHealth;
    }

    private void AnimationController()
    {
        double d = (double)currentHealth / (double)TotalHealth * 10;
        myAnimator.SetInteger("Health", (int)d);
    }

    
}
