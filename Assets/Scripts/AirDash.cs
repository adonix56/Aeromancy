using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDash : BaseSkill
{
    [SerializeField] private float dashStrength;

    private CharacterMovement characterMovement;
    private GameInput gameInput;

    public override void Activate() {
        //TODO: implement aeroCost and cooldown
        gameInput = GameInput.Instance;
        characterMovement = CharacterManager.Instance.GetCharacterMovement();
        Vector2 direction = gameInput.GetNormalizedMovement();
        characterMovement.HandleImpulse(direction, dashStrength);
    }

    public override void Deactivate() {
        Debug.Log("Deactivating AirDash");
        //Destroy(gameObject);
    }
}
