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
    private const string DAMAGE = "Damage";
    private const string DIE = "Die";

    [SerializeField] private SkillSO attack;
    [SerializeField] private SkillSO projectile;
    [SerializeField] private Transform fireballLocation;
    [SerializeField] private int damageTaken;
    [SerializeField] private Transform damageLocation;
    [SerializeField] private GameObject hitAnimation;
    [SerializeField, Range(0, 3)] private int venusFlytrapAttacks;
    [SerializeField, Range(0f, 40f)] private float visualDistance;
    [SerializeField, Range(0f, 40f)] private float projectileDistance;
    [SerializeField, Range(0f, 40f)] private float attackDistance;

    //private float attackCooldown;
    //private float projectileCooldown;
    //private float pause;
    private string nextAttack;
    private bool isHit;
    private bool isEaten;
    private Whirlwind whirlwindObject;
    private float waitToExplode = 2f;
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private VenusFlytrap flytrap;
    private bool isMad;
    private bool alive = true;
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
        if (health <= 0) {
            isMad = false;
            isHit = false;
            isEaten = false;
            alive = false;
            nav.isStopped = true;
            animator.SetBool(DAMAGE, false);
            animator.SetTrigger(DIE);
        } else if (!isHit) {
            animator.SetFloat(MOVE, nav.velocity.sqrMagnitude);
            if (inVisualRange) {
                nav.SetDestination(playerTransform.position);
                if (!nav.isStopped)
                    HandleAttacks();
            } else {
                nav.SetDestination(returnTransform.position);
            }
        } else {
            if (waitToExplode < 0) {
                flytrap.Explode();
                flytrap = null;
                transform.parent = null;
                transform.position = previousPosition;
                transform.rotation = previousRotation;
                isHit = false;
                isEaten = false;
                waitToExplode = 2;
                health--;
                if (--venusFlytrapAttacks <= 0) {
                    isMad = true;
                }
            } else if (!isEaten) {
                if (whirlwindObject) {
                    transform.position = whirlwindObject.transform.position;
                    if (whirlwindObject.isBurning && isMad) {
                        animator.SetBool(DAMAGE, true);
                    }
                } else {
                    isHit = false;
                    animator.SetBool(DAMAGE, false);
                }
            } else {
                waitToExplode -= Time.deltaTime;
                transform.position = transform.parent.position;
                transform.rotation = transform.parent.rotation;
            }
        }
        CalculateDistances();
        base.Update();
    }

    private void CalculateDistances() {
        float distance = Vector3.Distance(CharacterManager.Instance.transform.position, transform.position);
        inVisualRange = distance < visualDistance;
        inProjectileRange = distance < projectileDistance;
        inAttackRange = distance < attackDistance;
    }

    public bool IsAlive() {
        return alive;
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

    public void GetEaten(VenusFlytrap venusFlytrap) {
        previousPosition = transform.position;
        previousRotation = transform.rotation;
        flytrap = venusFlytrap;
        isEaten = true;
    }

    public override void TriggerProjectile() {
        Projectile fireball = Instantiate(projectile.skillPrefab, fireballLocation.position, Quaternion.identity).GetComponent<Projectile>();
        if (isMad) fireball.SetSpawnOnDeath();
    }

    public override void TriggerAttack() {
        if (inDamageRange) {
            characterHealth.GetHit();
        }
    }

    public override void TriggerDamage() {
        //Trigger Damage
        health -= damageTaken;
        Instantiate(hitAnimation, damageLocation);
    }

    public override void ProjectileHit(GameObject hit) {
        //Hit by whirlwind
        whirlwindObject = hit.GetComponent<Whirlwind>();
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
    public override void ResetTriggerStates() {
        inAttackRange = false;
        inProjectileRange = false;
        inVisualRange = false;
        inDamageRange = false;
    }
}
