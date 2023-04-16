using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float life = 1.0f;

    [HideInInspector]
    public Blowable blowable;
    private bool isBlowing;

    private void Awake()
    {
        blowable = GetComponent<Blowable>();
        blowable.OnBlowEnter += OnBlowEnter;
        blowable.OnBlowExit += OnBlowExit;
        blowable.OnBlowedOut += OnBlowedOut;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBlowing)
        {
            life -= Time.deltaTime;
        }
        if(life < 0)
        {
            blowable.BlowOut();
        }
    }

    public void OnBlowEnter(Vector3 direction)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        if (direction != Vector3.zero)
        {
            var lifetimeVel = ps.velocityOverLifetime;
            lifetimeVel.xMultiplier = direction.x;
            lifetimeVel.zMultiplier = direction.z;
        }
        isBlowing = true;
    }

    public void OnBlowExit()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var lifetimeVel = ps.velocityOverLifetime;
        lifetimeVel.xMultiplier = 0;
        lifetimeVel.zMultiplier = 0;
        isBlowing = false;
    }

    public void OnBlowedOut(Blowable blowable)
    {
        GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject, 1.5f);
    }
}
