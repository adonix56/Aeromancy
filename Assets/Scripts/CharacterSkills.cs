using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkills : MonoBehaviour
{
    [SerializeField] private SkillSO skill1;
    [SerializeField] private SkillSO skill2;

    private GameInput gameInput;

    private void Start() {
        gameInput = GameInput.Instance;

        gameInput.OnSkill1Action += GameInput_OnSkill1Action;
        gameInput.OnSkill2Action += GameInput_OnSkill2Action;
    }

    private void GameInput_OnSkill1Action(object sender, System.EventArgs e) {
        Instantiate(skill1.prefab, transform).GetComponent<BaseSkill>().Activate();
    }

    private void GameInput_OnSkill2Action(object sender, System.EventArgs e) {
        skill2.prefab.GetComponent<BaseSkill>().Activate();
    }
}
