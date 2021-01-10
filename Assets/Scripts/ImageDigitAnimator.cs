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

    public void SetImage(int digit)
    {
        int n = digit - 1;
        if (n < 0) n = digitImages.Length - 1;
        //print("DIGIT:  " + digit);
        image.sprite = digitImages[n];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
