using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFiresScript : MonoBehaviour
{
    private const string MOVING = "Moving";

    [SerializeField] private GameObject player;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] SkillSO skill;

    public void Play()
    {
        player.GetComponent<CharacterSkills>().AddSkill(0, skill);
        characterAnimator.SetFloat(MOVING, 0);
    }
}
