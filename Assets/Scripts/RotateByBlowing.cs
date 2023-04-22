using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByBlowing : MonoBehaviour
{
    public Vector3 targetRotationAngles;
    public Vector3 directionNeededToBlow;
    public float timeNeededToBlow = 1.0f;
    public float rotateAnimationTime = 1.0f;

    private float blowTimer;
    private float blowStrength;
    private bool isBeingBlown;

    private void Start()
    {
        isBeingBlown = false;
        blowTimer = 0;
        GetComponent<Blowable>().OnBlowEnter += Blow;
        GetComponent<Blowable>().OnBlowExit += StopBlow;
    }

    private void Update()
    {
        if(isBeingBlown)
        {
            blowTimer += Time.deltaTime * blowStrength;
            if(blowTimer > timeNeededToBlow)
            {
                Rotate();
            }
        }
    }

    public void Rotate()
    {
        transform.LeanRotate(targetRotationAngles, rotateAnimationTime).setEaseOutBounce();
    }

    public void Blow(Vector3 direction)
    {
        directionNeededToBlow = directionNeededToBlow.normalized;
        blowStrength = Vector3.Dot(direction, directionNeededToBlow);
        if (blowStrength > 0)
        {
            isBeingBlown = true;
        }
    }

    public void StopBlow()
    {
        isBeingBlown = false;
    }
}
