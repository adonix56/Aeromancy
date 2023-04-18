using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Whirlwind : MonoBehaviour
{
    public float duration = 1.0f;
    public float movementSpeed = 1.0f;
    public Vector3 direction;
    public VisualEffect whirlwindEffect;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        whirlwindEffect.SetFloat("Duration", duration);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > duration)
        {
            Destroy(gameObject);
        }

        transform.position += movementSpeed * direction * Time.deltaTime;
    }
}
