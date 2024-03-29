using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    private CharacterController characterController;
    private CharacterMovement characterMovement;
    private CharacterSkills characterSkills;
    private CharacterAnimation characterAnimation;
    private CharacterSpawn characterSpawn;
    private Rigidbody characterRigidbody;
    private bool isPlayable = true;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one CharacterManager created");
        }
        Instance = this;

        characterController = GetComponent<CharacterController>();
        characterMovement = GetComponent<CharacterMovement>();
        characterSkills = GetComponent<CharacterSkills>();
        characterAnimation = GetComponent<CharacterAnimation>();
        characterSpawn = GetComponent<CharacterSpawn>();
        characterRigidbody = GetComponent<Rigidbody>();
    }

    public bool IsPlayable() { return isPlayable; }

    public void SetPlayable(bool playable) { isPlayable = playable; }

    public CharacterController GetCharacterController() { return characterController; }
    public CharacterMovement GetCharacterMovement() { return characterMovement; }
    public CharacterSkills GetCharacterSkills() { return characterSkills; }
    public CharacterAnimation GetCharacterAnimation() { return characterAnimation; }
    public CharacterSpawn GetCharacterSpawn() { return characterSpawn; }
    public Rigidbody GetCharacterRigidbody() { return characterRigidbody; }
}
