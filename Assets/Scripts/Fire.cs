using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float life = 1.0f;
    private bool isBlowing;

    // Update is called once per frame
    void Update()
    {
        if(isBlowing)
        {
            life -= Time.deltaTime;
        }
        if(life < 0)
        {
            Extinguish();
        }
    }

    public void Blow(Vector3 direction, float strength)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (direction != Vector3.zero)
        {
            direction = direction.normalized * strength;
            var lifetimeVel = ps.velocityOverLifetime;
            lifetimeVel.xMultiplier = direction.x;
            lifetimeVel.zMultiplier = direction.z;
        }
        isBlowing = true;
    }

    public void StopBlow()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var lifetimeVel = ps.velocityOverLifetime;
        lifetimeVel.xMultiplier = 0;
        lifetimeVel.zMultiplier = 0;
        isBlowing = false;
    }

    public void Extinguish()
    {
        GetComponent<ParticleSystem>().Stop();
    }
}
