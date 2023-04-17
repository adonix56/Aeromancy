using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public const string MOVING = "Moving";

    [SerializeField] private Animator animator;

    public void Move(float move) {
        animator.SetFloat(MOVING, move);
    }
}
