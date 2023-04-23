using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprint : BaseSkill
{
    [SerializeField] private SkillSO sprintSO;
    private CharacterMovement characterMovement;
    private CharacterBreathLevel characterBreathLevel;

    private void Update() {
        // Restore if not moving
        if (!characterMovement.IsMoving()) {
            characterBreathLevel.GiveEnergy(sprintSO.energyCost);
        }
    }

    public override void Activate() {
        characterMovement = CharacterManager.Instance.GetCharacterMovement();
        characterBreathLevel = CharacterManager.Instance.GetCharacterBreathLevel();
        characterMovement.SetSprint(true);
    }

    public override void Deactivate() {
        characterMovement.SetSprint(false);
        Destroy(gameObject);
    }
}
