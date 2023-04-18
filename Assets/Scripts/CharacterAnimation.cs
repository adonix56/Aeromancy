using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public const string MOVING = "Moving";
    public const string BLOWING = "Blowing";

    [SerializeField] private Animator animator;

    public void Move(float move) {
        animator.SetFloat(MOVING, move*2);
    }

    public void SetBlow(bool blow) {
        animator.SetBool(BLOWING, blow);
    }
}
