using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public const string MOVING = "Moving";
    public const string BLOWING = "Blowing";
    public const string HOLDING = "Holding";

    [SerializeField] private Animator animator;

    public void Move(float move) {
        animator.SetFloat(MOVING, move);
        animator.transform.position = transform.position;
    }

    public void SetBlow(bool blow) {
        animator.SetBool(BLOWING, blow);
    }

    public void SetHold(bool hold) {
        animator.SetBool(HOLDING, hold);
    }
}
