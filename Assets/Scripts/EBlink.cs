using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBlink : MonoBehaviour
{

    SpriteRenderer myRenderer;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        myRenderer.enabled = false;
        myAnimator.enabled = false;
    }

    public void EnableEblink()
    {
        myRenderer.enabled = true;
        myAnimator.enabled = true;
    }
    public void DisableEblink()
    {
        myRenderer.enabled = false;
        myAnimator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        //if (transform.parent.transform.localScale.x < 0)
        //{
        //    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        //} 

    }
}
