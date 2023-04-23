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

    public void TriggerDamage() {
        enemy.TriggerDamage();
    }

    public void TriggerDeath() {
        enemy.TriggerDeath();
    }

    public void StartFollow() {
        enemy.StartFollow();
    }
}
