using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPup : Enemy {
    private const string BITE = "Bite";
    private const string MOVE = "Move";

    [SerializeField] private SkillSO attack;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        animator.SetFloat(MOVE, nav.velocity.sqrMagnitude);
        if (inVisualRange) {
            nav.SetDestination(playerTransform.position);
            if (!nav.isStopped)
                HandleAttacks();
        } else {
            nav.SetDestination(returnTransform.position);
        }
        base.Update();
    }

    private void HandleAttacks() {
        if (inAttackRange && attackCooldown < 0) {
            transform.LookAt(playerTransform);
            nav.isStopped = true;
            attackCooldown = attack.cooldown;
            animator.SetTrigger(BITE);
        }
    }

    public override void TriggerProjectile() {
        //Instantiate(projectile.skillPrefab, fireballLocation.position, Quaternion.identity).GetComponent<SnakeNagaFireball>();
    }

    public override void TriggerAttack() {
        if (inDamageRange) {
            characterHealth.GetHit();
        }
    }

    public override void TriggerDamage() {
        // One hit kills?
    }

    public override void ProjectileHit(GameObject hit) {
        //Hit by whirlwind
        //hitObject = hit;
        //isHit = true;
    }

    public override void StartFollow() {
        transform.position = animator.rootPosition;
        pause = pauseAfterAttack;
        //nextAttack = UnityEngine.Random.Range(0f, 1f) > 0.5f ? BITE : ATTACK;
        StartCoroutine(PauseBeforeMove());
    }

    private IEnumerator PauseBeforeMove() {
        yield return new WaitWhile(() => pause > 0);
        nav.isStopped = false;
    }
}
