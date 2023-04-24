using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerFairy : Enemy
{
    private const string MOVE = "Move";
    private const string ATTACK = "Seed";
    private const string LOOK_AROUND = "Look Around";
    private const string SPIN = "Spin";
    private const string DAMAGE = "Damage";
    private const string DIE = "Die";

    [SerializeField] private SkillSO projectile;
    [SerializeField] private Transform seedLocation;
    [SerializeField] private int damageTaken;
    [SerializeField] private GameObject hitAnimation;

    private bool alive = true;
    private bool isHit;
    private bool isBurning;
    private GameObject hitObject;
    private float waitToLookAround;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        //Debug.Log(health);
        if (health <= 0) {
            alive = false;
            isBurning = false;
            isHit = false;
            animator.SetBool(DAMAGE, isBurning);
            nav.isStopped = true;
            animator.SetTrigger(DIE);
        } else if (!isHit) {
            animator.SetFloat(MOVE, nav.velocity.sqrMagnitude);
            if (inVisualRange) {
                nav.SetDestination(playerTransform.position);
                if (!nav.isStopped)
                    HandleAttacks();
            } else {
                //nav.SetDestination(returnTransform.position);
            }
            if (waitToLookAround < 0) {
                animator.SetTrigger(LOOK_AROUND);
                waitToLookAround = Random.Range(3f, 5f);
            }
            waitToLookAround -= Time.deltaTime;
        } else if (isBurning) { 
            if (hitObject) {
                transform.position = hitObject.transform.position;
            } else {
                isBurning = false;
                isHit = false;
                animator.SetBool(DAMAGE, isBurning);
                StartFollow();
            }
        }
        base.Update();
    }

    public bool IsAlive() {
        return alive;
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

    public override void TriggerDamage() {
        health -= damageTaken;
        Instantiate(hitAnimation, transform);
    }

    public override void ProjectileHit(GameObject hit) {
        //Hit by whirlwind
        Whirlwind whirlwind = hit.GetComponent<Whirlwind>();
        if (whirlwind) {
            isBurning = whirlwind.isBurning;
            animator.SetBool(DAMAGE, isBurning);
            if (!isBurning)
                animator.SetTrigger(SPIN);
        }
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
    public override void ResetTriggerStates() {
        inAttackRange = false;
        inProjectileRange = false;
        inVisualRange = false;
        inDamageRange = false;
    }
}
