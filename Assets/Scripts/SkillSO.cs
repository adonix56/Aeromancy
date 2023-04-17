using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public GameObject skillPrefab;
    public bool holdingSkill;
    public bool skillSpawnOnParent;
    /*
    public string airSkillName;
    public GameObject airSkillPrefab;
    public bool holdingAirSkill;
    public bool airSkillSpawnOnParent;*/
}
