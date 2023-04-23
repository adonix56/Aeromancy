using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeCheck : MonoBehaviour {
    private const string PLAYER = "Player";
    public enum RangeType { Visual, Attack, Projectile, Damage};

    [SerializeField] private Enemy enemy;
    [SerializeField] private RangeType rangeType;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(PLAYER)) {
            if (rangeType == RangeType.Visual) {
                enemy.InVisualRange(true);
            } else if (rangeType == RangeType.Attack) {
                enemy.InAttackRange(true);
            } else if (rangeType == RangeType.Projectile) {
                enemy.InProjectileRange(true);
            } else if (rangeType == RangeType.Damage) {
                enemy.InDamageRange(true);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(PLAYER)) {
            if (rangeType == RangeType.Visual) {
                enemy.InVisualRange(false);
            } else if (rangeType == RangeType.Attack) {
                enemy.InAttackRange(false);
            } else if (rangeType == RangeType.Projectile) {
                enemy.InProjectileRange(false);
            } else if (rangeType == RangeType.Damage) {
                enemy.InDamageRange(false);
            }
        }
    }
}
