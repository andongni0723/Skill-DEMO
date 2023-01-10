using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowlyObj : MonoBehaviour
{
    public float fadeSpeed;

    private void Update()
    {
        FadeOut();
    }

    public void FadeOut()
    {
        Color objectColor = this.GetComponent<Renderer>().material.color;
        float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
        objectColor =  new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);

        this.GetComponent<Renderer>().material.color =  objectColor;
        if (objectColor.a <= 0)
            Destroy(gameObject);          
    }
}
