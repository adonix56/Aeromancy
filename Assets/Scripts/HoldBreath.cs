using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldBreath : BaseSkill
{
    public static bool isHoldingBreath;

    [SerializeField] private float moveSpeed;
    [SerializeField] private SkillSO holdBreathSO;

    private CharacterAnimation characterAnimation;
    private CharacterMovement characterMovement;
    private CharacterBreathLevel characterBreathLevel;

    private void Update() {
        characterBreathLevel.UseEnergy(holdBreathSO.energyCost);
    }

    public override void Activate() {
        characterAnimation = CharacterManager.Instance.GetCharacterAnimation();
        characterMovement = CharacterManager.Instance.GetCharacterMovement();
        characterBreathLevel = CharacterManager.Instance.GetCharacterBreathLevel();
        characterBreathLevel.LockRegen(true);
        characterMovement.SetPlayerSpeed(moveSpeed, true);
        isHoldingBreath = true;
    }

    public override void Deactivate() {
        isHoldingBreath = false;
        characterMovement.ResetPlayerSpeed();
        characterBreathLevel.LockRegen(false);
        Destroy(gameObject);
    }
}
