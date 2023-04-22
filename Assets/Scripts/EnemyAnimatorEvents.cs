using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorEvents : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    public void TriggerAttack() {
        enemy.TriggerAttack();
    }

    public void TriggerProjectile() {
        enemy.TriggerProjectile();
    }

    public void StartFollow() {
        enemy.StartFollow();
    }
}
