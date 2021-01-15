using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeLeafController : MonoBehaviour
{

    SpriteRenderer myRenderer;
    Collider2D myCollider;

    [SerializeField] Sprite[] mySprites;

    private Sprite previousState;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;
    }

    public void DefaultSprite()
    {
        myRenderer.sprite = mySprites[0];
    }


    private void OnMouseEnter()
    {
        previousState = myRenderer.sprite;
        myRenderer.sprite = mySprites[1];
    }

    private void OnMouseExit()
    {
        myRenderer.sprite = previousState;
    }

    private void OnMouseDown()
    {
        Debug.Log(transform.name + " was Clicked!!!!!!!!");
    }

    public void SelectedSprite()
    {
        myRenderer.sprite = mySprites[2];
        DisabelHoverCollider();
    }

    public void EnableHoverCollider()
    {
        myCollider.enabled = true;
    }

    public void DisabelHoverCollider()
    {
        myCollider.enabled = false;
    }
}
