using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public delegate void BurnEnter();
    public event BurnEnter OnBurnEnter;
    public delegate void BurnExit();
    public event BurnExit OnBurnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Fire>() != null && OnBurnEnter != null)
        {
            OnBurnEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Fire>() != null && OnBurnEnter != null)
        {
            OnBurnExit();
        }
    }
}
