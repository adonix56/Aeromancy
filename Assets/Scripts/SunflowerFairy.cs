using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerFairy : Enemy
{
    private const string MOVE = "Move";
    private const string ATTACK = "Seed";
    private const string LOOK_AROUND = "Look Around";
    private const string SPIN = "Spin";

    [SerializeField] private SkillSO projectile;
    [SerializeField] private Transform seedLocation;

    private bool isHit;
    private GameObject hitObject;
    private float waitToLookAround;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        if (!isHit) {
            animator.SetFloat(MOVE, nav.velocity.sqrMagnitude);
            if (inVisualRange) {
                nav.SetDestination(playerTransform.position);
                if (!nav.isStopped)
                    HandleAttacks();
            } else {
                nav.SetDestination(returnTransform.position);
            }
            if (waitToLookAround < 0) {
                animator.SetTrigger(LOOK_AROUND);
                waitToLookAround = Random.Range(3f, 5f);
            }
            waitToLookAround -= Time.deltaTime;
        } else { 
            if (hitObject) {
                animator.SetTrigger(SPIN);
                hitObject = null;
            }
        }
        base.Update();
    }

    private void HandleAttacks() {
        if (inProjectileRange && projectileCooldown < 0) {
            transform.LookAt(playerTransform);
            nav.isStopped = true;
            projectileCooldown = projectile.cooldown;
            animator.SetTrigger(ATTACK);
        }
    }

    public override void TriggerAttack() {
        // No Melee attacks - Used as a Spin Animation event
        isHit = false;
        StartFollow();
    }

    public override void TriggerProjectile() {
        Instantiate(projectile.skillPrefab, seedLocation.position, Quaternion.identity);
    }

    public override void ProjectileHit(GameObject hit) {
        //Hit by whirlwind
        hitObject = hit;
        isHit = true;
    }

    public override void StartFollow() {
        pause = pauseAfterAttack;
        //nextAttack = UnityEngine.Random.Range(0f, 1f) > 0.5f ? BITE : ATTACK;
        StartCoroutine(PauseBeforeMove());
    }

    private IEnumerator PauseBeforeMove() {
        yield return new WaitWhile(() => pause > 0);
        nav.isStopped = false;
    }
}
