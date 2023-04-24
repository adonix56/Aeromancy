using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPup : Enemy {
    private const string BITE = "Bite";
    private const string MOVE = "Move";
    private const string DIE = "Die";

    [SerializeField] private SkillSO attack;

    private VenusFlytrap venusFlytrap;
    private bool alive = true;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        if (alive) {
            animator.SetFloat(MOVE, nav.velocity.sqrMagnitude);
            if (inVisualRange) {
                nav.SetDestination(playerTransform.position);
                if (!nav.isStopped)
                    HandleAttacks();
            } else {
                nav.SetDestination(returnTransform.position);
            }
            base.Update();
        } else {
            transform.position = transform.parent.position;
            transform.rotation = transform.parent.rotation;
        }
    }

    public bool IsAlive() {
        return alive;
    }

    private void HandleAttacks() {
        if (inAttackRange && attackCooldown < 0) {
            transform.LookAt(playerTransform);
            nav.isStopped = true;
            attackCooldown = attack.cooldown;
            animator.SetTrigger(BITE);
        }
    }

    public void SetVenusFlytrap(VenusFlytrap vft) {
        venusFlytrap = vft;
    }

    public override void TriggerProjectile() {
        //Instantiate(projectile.skillPrefab, fireballLocation.position, Quaternion.identity).GetComponent<SnakeNagaFireball>();
    }

    public override void TriggerAttack() {
        if (venusFlytrap && !venusFlytrap.HasEaten()) {
            nav.isStopped = true;
            animator.SetTrigger(DIE);
            alive = false;
            venusFlytrap.Eat(this, "Wolf Pup");
        }
        if (inDamageRange && CheckIfPlayerIsCloser()) {
            characterHealth.GetHit();
        }
        //Weird at the end of the trigger... It says that the player 
    }

    private bool CheckIfPlayerIsCloser() {
        if (venusFlytrap) {
            float venusDistance = Vector3.Distance(venusFlytrap.transform.position, transform.position);
            float playerDistance = Vector3.Distance(characterHealth.transform.position, transform.position);
            return venusDistance > playerDistance;
        }
        return true;
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
