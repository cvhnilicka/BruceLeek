using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterController : MonoBehaviour
{
    [SerializeField] int largeHitValue = 3;
    [SerializeField] int smallHitValue = 1;

    [SerializeField] float Total = 10f;
    float currentAmount;

    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        currentAmount = Total;
    }

    // Update is called once per frame
    void Update()
    {
        AnimationController();
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    public float GetCurrentAmount()
    {
        return currentAmount;
    }

    void RenderCheck()
    {
        if (currentAmount <= 0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;

        }

    }

    public void ResetTotal(float total)
    {
        this.Total = total;
        currentAmount = Total;
        RenderCheck();
        AnimationController();

    }

    public float ReduceMeter(float reduction)
    {
        currentAmount -= reduction;
        AnimationController();
        RenderCheck();
        return currentAmount;
    }

    private void AnimationController()
    {
        if (myAnimator != null)
            myAnimator.SetFloat("Percentage", currentAmount/Total);
    }

}
