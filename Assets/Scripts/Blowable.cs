using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blowable : MonoBehaviour
{
    public delegate void BlowEnter(Vector3 blowDirection);
    public event BlowEnter OnBlowEnter;
    public delegate void BlowExit();
    public event BlowExit OnBlowExit;
    public delegate void BlowedOut(Blowable go);
    public event BlowedOut OnBlowedOut;

    public void TriggerBlowEnter(Vector3 blowDirection)
    {
        if(OnBlowEnter != null)
        {
            OnBlowEnter(blowDirection);
        }
    }

    public void TriggerBlowExit()
    {
        if(OnBlowExit != null)
        {
            OnBlowExit();
        }
    }
    public void BlowOut()
    {
        OnBlowedOut(this);
        //Destroy(gameObject);
    }
}
