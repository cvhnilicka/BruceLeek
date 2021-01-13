using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    BoxCollider2D myCollider;

    [SerializeField] float attackTime = .2f;
    float attackTimer;
    float attackCountdown;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myCollider.enabled = false;
        attackCountdown = 99f;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackCountdown >= attackTime)
        {
            AttackDeactivate();
        }
        attackCountdown += Time.deltaTime;
    }

    public void AttackActivate()
    {
        if (attackCountdown >= attackTime)
        {
            //Debug.Log("Attacking");
            myCollider.enabled = true;
            attackCountdown = 0f;
            //attackTimer = attackTime;
        }   
    }

    private void AttackDeactivate()
    {
        myCollider.enabled = false;
    }



}
