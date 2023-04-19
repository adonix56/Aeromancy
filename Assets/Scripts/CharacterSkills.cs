using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkills : MonoBehaviour
{
    [SerializeField] private SkillSO[] skill;
    [SerializeField] private SkillSO[] airSkill;
    public EnergyHandler energyHandler;

    private CharacterController characterController;
    private GameInput gameInput;
    private BaseSkill[] skillHeld;
    private BaseSkill[] airSkillHeld;

    private void Awake() {
        //characterController = GameObject.Find("Player").GetComponent<CharacterController>();
        skillHeld = new BaseSkill[2];
        airSkillHeld = new BaseSkill[2];
    }

    private void Start() {
        gameInput = GameInput.Instance;
        characterController = CharacterManager.Instance.GetCharacterController();

        gameInput.OnSkill0Action += GameInput_OnSkill0Action;
        gameInput.OnSkill1Action += GameInput_OnSkill1Action;
    }

    private void Update() {
        if (CharacterManager.Instance.IsPlayable()) {
            //For Holding Skills
            CheckSkill(0); // Checking Skill 0
            CheckSkill(1); // Checking Skill 1
        }
    }

    private void CheckSkill(int index) { 
        if (gameInput.isSkillPressed(index)) {
            if (characterController.isGrounded) {
                energyHandler.UseEnergy(skill[index].energyCost);
                // Deactivate Holding Air Skill if applicable
                if (airSkillHeld[index] != null) DeactivateAirSkill(index);
                // Activate Holding Ground Skill
                if (skill[index].holdingSkill && skillHeld[index] == null) {
                    Transform parent = skill[index].skillSpawnOnParent ? transform : null;
                    skillHeld[index] = Instantiate(skill[index].skillPrefab, parent).GetComponent<BaseSkill>();
                    skillHeld[index].Activate();
                }
            } else {
                energyHandler.UseEnergy(airSkill[index].energyCost);
                // Deactivate Holding Ground Skill if applicable
                if (skillHeld[index] != null) DeactivateSkill(index);
                // Activate Holding Air Skill
                if (airSkill[index].holdingSkill && airSkillHeld[index] == null) {}
            }
            
        } else {
            DeactivateSkill(index);
            DeactivateAirSkill(index);
        }
    }

    private void DeactivateSkill(int index) {
        if (skillHeld[index] != null) {
            skillHeld[index].Deactivate();
            skillHeld[index] = null;
        }
    }

    private void DeactivateAirSkill(int index) {
        if (airSkillHeld[index] != null) {
            airSkillHeld[index].Deactivate();
            airSkillHeld[index] = null;
        }
    }

    //For Non-Holding Skills
    private void GameInput_OnSkill0Action(object sender, System.EventArgs e) {
        if (CharacterManager.Instance.IsPlayable()) {
            //TODO: Setup Skill Args to determine to activate skill on character or world space:
            //      i.e. AirDash in world space, shooting skill on character
            if (!characterController.isGrounded && !airSkill[0].holdingSkill) {
                energyHandler.UseEnergy(airSkill[0].energyCost);
                Instantiate(airSkill[0].skillPrefab, transform.position, Quaternion.identity).GetComponent<BaseSkill>().Activate();
            }
            if (characterController.isGrounded && !skill[0].holdingSkill) {
                energyHandler.UseEnergy(skill[0].energyCost);
                Instantiate(skill[0].skillPrefab, transform.position, Quaternion.identity).GetComponent<BaseSkill>().Activate();
            }
        }
    }

    private void GameInput_OnSkill1Action(object sender, System.EventArgs e) {
        if (CharacterManager.Instance.IsPlayable()) {
            if (!characterController.isGrounded && !airSkill[1].holdingSkill) {
                energyHandler.UseEnergy(airSkill[1].energyCost);
                Instantiate(airSkill[1].skillPrefab, transform.position, Quaternion.identity).GetComponent<BaseSkill>().Activate();
            }
            if (characterController.isGrounded && !skill[1].holdingSkill) {
                energyHandler.UseEnergy(skill[1].energyCost);
                Instantiate(skill[1].skillPrefab, transform.position, Quaternion.identity).GetComponent<BaseSkill>().Activate();
            }
        }
    }
}
