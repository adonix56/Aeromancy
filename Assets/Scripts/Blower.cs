using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : BaseSkill
{
    public float blowStrength;
    public Collider blowCollider;
    public Dictionary<int, Blowable> blowables;

    [SerializeField] private float blowSpeed;

    private CharacterAnimation characterAnimation;
    private CharacterMovement characterMovement;

    private void Awake() {
        blowables = new Dictionary<int, Blowable>();
    }

    private void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        /*/Debug.Log(gameInput.isSkill1Pressed());
        if (Input.GetMouseButton(0))
        {
            TurnOn(true);
        }
        else
        {
            TurnOn(false);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        Blowable blowableObject = other.GetComponent<Blowable>();
        if (blowableObject && blowCollider.bounds.Intersects(other.bounds))
        {
            blowables.Add(blowableObject.GetInstanceID(), blowableObject);
            Vector3 directionToFire = blowableObject.transform.position - transform.position;
            blowableObject.TriggerBlowEnter(directionToFire.normalized * blowStrength);
        }
    }

    private void OnTriggerExit(Collider other) {
        Blowable blowableObject = other.GetComponent<Blowable>();
        if (blowableObject)
        {
            blowables.Remove(blowableObject.GetInstanceID());
            blowableObject.TriggerBlowExit();
        }
    }

    /*public void TurnOn(bool turnOn)
    {
        blowCollider.enabled = turnOn;

        if (!turnOn)
        {
            foreach (Blowable blowableObject in GameObject.FindObjectsOfType<Blowable>())
            {
                blowableObject.TriggerBlowExit();
            }
        }
    }*/

    public override void Activate() {
        characterAnimation = CharacterManager.Instance.GetCharacterAnimation();
        characterMovement = CharacterManager.Instance.GetCharacterMovement();
        characterAnimation.SetBlow(true);
        characterMovement.SetPlayerSpeed(blowSpeed, true);
    }

    public override void Deactivate() {
        characterAnimation.SetBlow(false);
        characterMovement.ResetPlayerSpeed();
        foreach (Blowable blowable in blowables.Values) {
            blowable.TriggerBlowExit();
        }
        Destroy(gameObject);
    }
}
