using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    [SerializeField] AnimationClip deathClip;


    Rigidbody2D enemyRigidbody;
    BoxCollider2D attackCollider;
    Animator myAnimator;
    CapsuleCollider2D myCollider;
    HealthBar healthBar;
    GameObject player;
    MeleeAttack meleeAttack;

    private float attackTimer;
    private float TimeBetweenAttacks = 1.5f;
    private bool walk;


    float damageTime = 1.5f;
    float damageTimer;


    // Start is called before the first frame update
    void Start()
    {

        enemyRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        attackCollider = GetComponent<BoxCollider2D>();
        myCollider = GetComponent<CapsuleCollider2D>();
        healthBar = GetComponentInChildren<HealthBar>();
        meleeAttack = GetComponentInChildren<MeleeAttack>();
        walk = true;
        damageTimer = 99f;
        player = GameObject.FindGameObjectWithTag("Player")
            .GetComponentInChildren<MovementController>().gameObject; ;

    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        if (walk) Walk();
        WalkAnimation();
        attackTimer += Time.deltaTime;
        damageTimer += Time.deltaTime;

    }

    private void Walk()
    {
        if (IsFacingRight())
        {
            enemyRigidbody.velocity = new Vector2(moveSpeed, enemyRigidbody.velocity.y);
        }
        else
        {
            enemyRigidbody.velocity = new Vector2(-moveSpeed, enemyRigidbody.velocity.y);
        }
    }

    private void LookAtPlayer()
    {
        if (player.transform.localPosition.x > transform.localPosition.x)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyRigidbody.velocity = new Vector2(moveSpeed, 0f);
        //transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody.velocity.x)), 1f);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void WalkAnimation()
    {
        if (Mathf.Abs(enemyRigidbody.velocity.x) > Mathf.Epsilon)
        {
            myAnimator.SetBool("Walking", true);
        }
        else
        {
            myAnimator.SetBool("Walking", false);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackCollider.IsTouchingLayers(LayerMask.GetMask("player")))
        {
            
            if (attackTimer >= TimeBetweenAttacks)
             {
                myAnimator.SetTrigger("Attack");
                meleeAttack.AttackActivate();
                attackTimer = 0f;
            }
            walk = false;
            enemyRigidbody.velocity = new Vector2(0f, enemyRigidbody.velocity.y);
            myAnimator.SetBool("Walking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = false;
        try
        {
            isPlayer = collision.gameObject.transform.parent.tag == "Player";
        }
        catch (Exception e)
        {
            //Debug.Log("Did not detect player");
        }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("weapon")) && isPlayer)
        {

            if (damageTimer >= damageTime)
            {
                Hit(collision);
                damageTimer = 0f;
            }
        }

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("friendlyBullet")))
        {
            Hit(collision);
            Destroy(collision.gameObject);
            damageTimer = 0f;
        }
    }

    private void Die()
    {
        myAnimator.SetTrigger("Death");
        GameObject.Find("GameSession").GetComponent<GameSession>().UpdateUIScore(250);
        Destroy(this.gameObject, deathClip.length);
    }

    private void Hit(Collider2D collision)
    {
        int health = healthBar.TakeDamage(collision.gameObject.GetComponent<IsWeapon>().GetWeaponDamage());
        if (health <= 0)
        {
            // die
            Die();
        }
    }

    //public void TakeDamage()
    //{
    //    if (damageTimer >= damageTime)
    //    {
    //        int health = healthBar.LargeHit();
    //        if (health <= 0)
    //        {
    //            // die
    //            myAnimator.SetTrigger("Death");
    //            Destroy(this.gameObject, deathClip.length);
    //        }
    //        damageTimer = 0f;
    //    }
    //}



}
