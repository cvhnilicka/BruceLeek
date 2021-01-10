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
    }

    void RenderCheck()
    {
        if (currentHealth <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
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
        myAnimator.SetInteger("Health", currentHealth);
    }

    
}
