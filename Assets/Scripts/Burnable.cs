using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public delegate void BurnEnter();
    public event BurnEnter OnBurnEnter;
    public delegate void BurnExit();
    public event BurnExit OnBurnExit;
    public Collider onlyListenToThisCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Fire>() != null && OnBurnEnter != null) 
        {
            if (OnBurnEnter != null)
            {
                if (onlyListenToThisCollider != null)
                {
                    if (other.bounds.Intersects(onlyListenToThisCollider.bounds))
                    {
                        OnBurnEnter();
                    }
                }
                else
                {
                    OnBurnEnter();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Fire>() != null && OnBurnExit != null)
        {
            if (OnBurnExit != null)
                OnBurnExit();
        }
    }
}
