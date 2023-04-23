using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkills : MonoBehaviour
{
    [SerializeField] private SkillSO[] skill;
    //[SerializeField] private SkillSO[] airSkill;

    private CharacterController characterController;
    private CharacterBreathLevel characterBreathLevel;
    private GameInput gameInput;
    private BaseSkill[] skillHeld;
    //private BaseSkill[] airSkillHeld;

    private void Awake() {
        //characterController = GameObject.Find("Player").GetComponent<CharacterController>();
        //0 LMB - 1 RMB - 2 SPACE - 3 SHIFT - 4 F
        skillHeld = new BaseSkill[5];
        //airSkillHeld = new BaseSkill[2];
    }

    private void Start() {
        gameInput = GameInput.Instance;
        characterController = CharacterManager.Instance.GetCharacterController();
        characterBreathLevel = GetComponent<CharacterBreathLevel>();

        // Left Mouse Button
        gameInput.OnSkill0Action += GameInput_OnSkill0Action;
        // Right Mouse Button
        gameInput.OnSkill1Action += GameInput_OnSkill1Action;
        // Spacebar
        gameInput.OnSkill2Action += GameInput_OnSkill2Action;
        // Shift Key
        gameInput.OnSkill3Action += GameInput_OnSkill3Action;
        // F Key
        gameInput.OnSkill4Action += GameInput_OnSkill4Action;
    }

    private void Update() {
        if (CharacterManager.Instance.IsPlayable()) {
            //For Holding Skills
            CheckSkill(0); // Checking LMB
            CheckSkill(1); // Checking RMB
            CheckSkill(2); // Checking Space
            CheckSkill(3); // Checking Shift
            CheckSkill(4); // Checking F
        }
    }

    private void CheckSkill(int index) { 
        if (gameInput.isSkillPressed(index)) {
            if (characterController.isGrounded) {
                characterBreathLevel.UseEnergy(skill[index].energyCost);
                // Deactivate Holding Air Skill if applicable
                //if (airSkillHeld[index] != null) DeactivateAirSkill(index);
                // Activate Holding Ground Skill
                if (skill[index].holdingSkill && skillHeld[index] == null) {
                    Transform parent = skill[index].skillSpawnOnParent ? transform : null;
                    skillHeld[index] = Instantiate(skill[index].skillPrefab, parent).GetComponent<BaseSkill>();
                    skillHeld[index].Activate();
                }
            }
            // Character can no longer jump *at this time
            /*else {
                characterBreathLevel.UseEnergy(airSkill[index].energyCost);
                // Deactivate Holding Ground Skill if applicable
                if (skillHeld[index] != null) DeactivateSkill(index);
                // Activate Holding Air Skill
                if (airSkill[index].holdingSkill && airSkillHeld[index] == null) {}
            }*/
            
        } else {
            DeactivateSkill(index);
            //DeactivateAirSkill(index);
        }
    }

    private void DeactivateSkill(int index) {
        if (skillHeld[index] != null) {
            skillHeld[index].Deactivate();
            skillHeld[index] = null;
        }
    }

    /*private void DeactivateAirSkill(int index) {
        if (airSkillHeld[index] != null) {
            airSkillHeld[index].Deactivate();
            airSkillHeld[index] = null;
        }
    }*/

    //For Non-Holding Skills
    private void GameInput_OnSkill0Action(object sender, System.EventArgs e) { ActivateNonHoldingSkill(0); }
    private void GameInput_OnSkill1Action(object sender, System.EventArgs e) { ActivateNonHoldingSkill(1); }
    private void GameInput_OnSkill2Action(object sender, System.EventArgs e) { ActivateNonHoldingSkill(2); }
    private void GameInput_OnSkill3Action(object sender, System.EventArgs e) { ActivateNonHoldingSkill(3); }
    private void GameInput_OnSkill4Action(object sender, System.EventArgs e) { ActivateNonHoldingSkill(4); }

    private void ActivateNonHoldingSkill(int index) {
        if (CharacterManager.Instance.IsPlayable()) {
            //TODO: Setup Skill Args to determine to activate skill on character or world space:
            //      i.e. AirDash in world space, shooting skill on character
            //if (!characterController.isGrounded && !airSkill[1].holdingSkill)
            //{
            //    characterBreathLevel.UseEnergy(airSkill[1].energyCost);
            //    Instantiate(airSkill[1].skillPrefab, transform.position, Quaternion.identity).GetComponent<BaseSkill>().Activate();
            //}
            if (characterController.isGrounded && !skill[index].holdingSkill) {
                characterBreathLevel.UseEnergy(skill[index].energyCost);
                Instantiate(skill[1].skillPrefab, transform.position, Quaternion.identity).GetComponent<BaseSkill>().Activate();
            }
        }
    }

    public void AddSkill(int index, SkillSO newSkill)
    {
        skill[index] = newSkill;
    }
}
