using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeCheck : MonoBehaviour {
    private const string PLAYER = "Player";
    private const string FLYTRAP = "Flytrap";
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
        } else if (rangeType == RangeType.Damage && other.CompareTag(FLYTRAP)) {
            WolfPup wolfpup;
            try {
                wolfpup = (WolfPup)enemy;
                if (wolfpup) {
                    wolfpup.SetVenusFlytrap(other.gameObject.GetComponent<VenusFlytrap>());
                }
            } catch {
                Debug.LogWarning("Can't Cast Wolfpup");
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag(PLAYER) || other.CompareTag(FLYTRAP)) {
            if (rangeType == RangeType.Visual) {
                enemy.InVisualRange(false);
            } else if (rangeType == RangeType.Attack) {
                enemy.InAttackRange(false);
            } else if (rangeType == RangeType.Projectile) {
                enemy.InProjectileRange(false);
            } else if (rangeType == RangeType.Damage) {
                enemy.InDamageRange(false);
            }
        } else if (rangeType == RangeType.Damage && other.CompareTag(FLYTRAP)) {
            WolfPup wolfpup;
            try {
                wolfpup = (WolfPup)enemy;
                if (wolfpup) {
                    wolfpup.SetVenusFlytrap(null);
                }
            } catch {
                Debug.LogWarning("Can't Cast Wolfpup");
            }
        }
    }
}
