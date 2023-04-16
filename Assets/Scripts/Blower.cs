using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
    public float blowStrength;
    public Collider blowCollider;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TurnOn(true);
        }
        else
        {
            TurnOn(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Blowable blowableObject = other.GetComponent<Blowable>();
        if (blowableObject && blowCollider.bounds.Intersects(other.bounds))
        {
            Vector3 directionToFire = blowableObject.transform.position - transform.position;
            blowableObject.TriggerBlowEnter(directionToFire.normalized * blowStrength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Blowable blowableObject = other.GetComponent<Blowable>();
        if (blowableObject)
        {
            blowableObject.TriggerBlowExit();
        }
    }

    public void TurnOn(bool turnOn)
    {
        blowCollider.enabled = turnOn;

        if (!turnOn)
        {
            foreach (Blowable blowableObject in GameObject.FindObjectsOfType<Blowable>())
            {
                blowableObject.TriggerBlowExit();
            }
        }
    }
}
