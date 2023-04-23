using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnakeNaga : Enemy {
    private const string MOVE = "Move";
    private const string PROJECTILE = "Projectile";
    private const string ATTACK = "Attack";
    private const string BITE = "Bite";

    [SerializeField] private SkillSO attack;
    [SerializeField] private SkillSO projectile;
    [SerializeField] private Transform fireballLocation;

    //private float attackCooldown;
    //private float projectileCooldown;
    //private float pause;
    private string nextAttack;
    private bool isHit;
    private bool isEaten;
    private GameObject hitObject;
    private float waitToExplode;
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    //private CharacterHealth characterHealth;
    /*[SerializeField] private Transform playerTransform;
    [SerializeField] private Transform returnTransform;

    private NavMeshAgent nav;
    private int health;
    private bool inVisualRange;
    private bool inAttackRange;
    private bool inProjectileRange;*/

    protected override void Start() {
        base.Start();
        nextAttack = UnityEngine.Random.Range(0f, 1f) > 0.5f ? BITE : ATTACK;
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
        } else {
            if (!isEaten) {
                if (hitObject) {
                    transform.position = hitObject.transform.position;
                } else {
                    isHit = false;
                }
            } else {
                transform.position = transform.parent.position;
                transform.rotation = transform.parent.rotation;
            }
        }
        base.Update();
    }

    private void HandleAttacks() {
        if (inProjectileRange && projectileCooldown < 0) {
            HandleAttackActivation(PROJECTILE, projectile, true);
        }
        if (inAttackRange && projectileCooldown > 0 && attackCooldown < 0) { // Only attack if fireball is on cooldown
            HandleAttackActivation(nextAttack, attack, false);
        }
    }

    private void HandleAttackActivation(string trigger, SkillSO skillSO, bool isProjectile) {
        transform.LookAt(playerTransform);
        nav.isStopped = true;
        if (isProjectile) projectileCooldown = skillSO.cooldown;
        else attackCooldown = skillSO.cooldown;
        animator.SetTrigger(trigger);
    }

    public bool IsInWhirlwind() {
        return isHit;
    }

    public void GetEaten() {
        previousPosition = transform.position;
        previousRotation = transform.rotation;
        isEaten = true;
    }

    public override void TriggerProjectile() {
        Instantiate(projectile.skillPrefab, fireballLocation.position, Quaternion.identity).GetComponent<Projectile>();
    }

    public override void TriggerAttack() {
        if (inDamageRange) {
            characterHealth.GetHit();
        }
    }

    public override void TriggerDamage() {
        //Trigger Damage
    }

    public override void ProjectileHit(GameObject hit) {
        //Hit by whirlwind
        hitObject = hit;
        isHit = true;
    }

    public override void StartFollow() {
        pause = pauseAfterAttack;
        nextAttack = UnityEngine.Random.Range(0f, 1f) > 0.5f ? BITE : ATTACK;
        StartCoroutine(PauseBeforeMove());
    }

    private IEnumerator PauseBeforeMove() {
        yield return new WaitWhile(() => pause > 0);
        nav.isStopped = false;
    }
}
