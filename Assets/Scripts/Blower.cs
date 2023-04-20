using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : BaseSkill
{
    public float blowStrength;
    public Collider blowCollider;

    [SerializeField] private float blowSpeed;

    private CharacterAnimation characterAnimation;
    private CharacterMovement characterMovement;

    private void Start() {

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

    private void OnTriggerExit(Collider other) {
        Blowable blowableObject = other.GetComponent<Blowable>();
        if (blowableObject)
        {
            blowableObject.TriggerBlowExit();
        }
    }

    public override void Activate() {
        characterAnimation = CharacterManager.Instance.GetCharacterAnimation();
        characterMovement = CharacterManager.Instance.GetCharacterMovement();
        characterAnimation.SetBlow(true);
        characterMovement.SetPlayerSpeed(blowSpeed, true);
        characterMovement.SetTurning(false);
    }

    public override void Deactivate() {
        characterAnimation.SetBlow(false);
        characterMovement.ResetPlayerSpeed();
        characterMovement.SetTurning(true);
        foreach (Blowable blowable in GameObject.FindObjectsOfType<Blowable>()) {
            blowable.TriggerBlowExit();
        }
        Destroy(gameObject);
    }
}
