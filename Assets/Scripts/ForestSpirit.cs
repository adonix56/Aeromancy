using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestSpirit : MonoBehaviour
{
    public float disparitionTime = 2.0f;

    public void Disappear()
    {
        ParticleSystem[] ps_list = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem ps in ps_list)
        {
            ParticleSystem.EmissionModule em = ps.emission;
            em.rateOverTimeMultiplier = 0;
            var sz = ps.sizeOverLifetime;
            sz.enabled = true;
            
        }
        Destroy(gameObject, disparitionTime);
    }
}
