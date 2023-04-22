using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flicker : MonoBehaviour
{
    public float flickerPeriod = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Image image = GetComponent<Image>();
        Color color = image.color;
        LeanTween.value(1.0f, 0.1f, flickerPeriod).setEaseInOutQuad().setLoopPingPong().setOnUpdate(
            (float value) =>
            {
                color.a = value;
                image.color = color;
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
