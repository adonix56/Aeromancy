using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : BaseSkill
{
    public float blowStrength;
    public Collider blowCollider;

    [SerializeField] private float blowSpeed;
    [SerializeField] private AudioClip blowingAudio;

    private CharacterAnimation characterAnimation;
    private CharacterMovement characterMovement;
    private CharacterBreathLevel characterBreathLevel;
    private AudioSource playerAudioSource;

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
        characterBreathLevel = CharacterManager.Instance.GetCharacterBreathLevel();
        playerAudioSource = CharacterManager.Instance.GetCharacterAudioSource();
        characterAnimation.SetBlow(true);
        characterBreathLevel.LockRegen(true);
        characterMovement.SetPlayerSpeed(blowSpeed, true);
        characterMovement.SetTurning(false);

        playerAudioSource.clip = blowingAudio;
        playerAudioSource.volume = 0.3f;
        playerAudioSource.Play();
    }

    public override void Deactivate() {
        characterAnimation.SetBlow(false);
        characterBreathLevel.LockRegen(false);
        characterMovement.ResetPlayerSpeed();
        characterMovement.SetTurning(true);
        playerAudioSource.volume = 1;
        playerAudioSource.Stop();
        foreach (Blowable blowable in GameObject.FindObjectsOfType<Blowable>()) {
            blowable.TriggerBlowExit();
        }
        Destroy(gameObject);
    }
}
