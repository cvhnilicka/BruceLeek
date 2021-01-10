using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{

    BoxCollider2D myCollider;

    [SerializeField] float attackTime = .5f;
    [SerializeField] float attackTimer;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myCollider.enabled = false;
        attackTimer = 99f;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer >= attackTime)
        {
            AttackDeactivate();
        }
        attackTimer += Time.deltaTime;
    }

    public void AttackActivate()
    {
        if (attackTimer >= attackTime)
        {
            Debug.Log("Attacking");
            myCollider.enabled = true;
            attackTimer = 0f;
        }   
    }

    private void AttackDeactivate()
    {
        myCollider.enabled = false;
    }



}
