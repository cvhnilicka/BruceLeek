using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageDigitAnimator : MonoBehaviour
{

    public Sprite[] digitImages;


    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

    }

    private void Awake()
    {
        image = GetComponent<Image>();

    }
    public void SetImage(int digit)
    {
        int n = digit - 1;
        if (n < 0) n = digitImages.Length - 1;
        // sometimes image.sprite is null
        // will need to look into later
        try
        {
            if (image.sprite) image.sprite = digitImages[n];

        }
        catch (Exception e )
        {
            Debug.LogWarning(gameObject.name + " is having issues");
            Debug.LogWarning(e.ToString());
        }
        

    }

}
