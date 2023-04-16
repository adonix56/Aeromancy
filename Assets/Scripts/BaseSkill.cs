using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    public class SkillEventArgs : EventArgs {
        public bool characterParent;
    }

    public abstract void Activate();
    public abstract void Deactivate();
}
