using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstFiresScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] SkillSO skill;

    public void Play()
    {
        player.GetComponent<CharacterSkills>().AddSkill(0, skill);
    }
}
