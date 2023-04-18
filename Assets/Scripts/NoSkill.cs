using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSkill : BaseSkill
{
    public override void Activate() {
        Deactivate();
    }

    public override void Deactivate() {
        Destroy(gameObject);
    }
}
