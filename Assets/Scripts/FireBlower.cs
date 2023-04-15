using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlower : MonoBehaviour
{
    public float blowStrength;
    public Collider blowCollider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TurnOn(true);
        } else
        {
            TurnOn(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Fire fire = other.GetComponent<Fire>();
        if (fire && blowCollider.bounds.Intersects(other.bounds))
        {
            Vector3 directionToFire = fire.transform.position - transform.position;
            fire.Blow(directionToFire, blowStrength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Fire fire = other.GetComponent<Fire>();
        if (fire)
        {
            fire.StopBlow();
        }
    }

    public void TurnOn(bool turnOn)
    {
        blowCollider.enabled = turnOn;

        if(!turnOn)
        {
            foreach (Fire fire in GameObject.FindObjectsOfType<Fire>())
            {
                fire.StopBlow();
            }
        }
    }
}
