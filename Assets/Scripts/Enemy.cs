using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _returnTransform;
    [SerializeField] private Animator _animator;
    [SerializeField, Range(0f, 10f)] private float _pauseAfterAttack;
    [SerializeField, Range(1f, 10f)] private int _health;

    protected Transform playerTransform { get { return _playerTransform; } set { _playerTransform = value; } }
    protected Transform returnTransform { get { return _returnTransform; } set { _returnTransform = value; } }
    protected NavMeshAgent nav { get; set; }
    protected Animator animator { get { return _animator; } set { _animator = value; } }
    protected float pauseAfterAttack { get { return _pauseAfterAttack; } set { _pauseAfterAttack = value; } }
    protected int health { get { return _health; } set { _health = value; } }
    protected bool inVisualRange { get; set; }
    protected bool inAttackRange { get; set; }
    protected bool inProjectileRange { get; set; }
    protected bool inDamageRange { get; set; }
    protected float pause { get; set; }
    protected CharacterHealth characterHealth { get; set; }
    protected float attackCooldown { get; set; }
    protected float projectileCooldown { get; set; }

    /*private void Start() {
        playerTransform = CharacterManager.Instance.transform;
        nav = GetComponent<NavMeshAgent>();
        //nav.enabled = false;
        //nav.enabled = true;
        //nav.Warp(transform.position);
    }

    private void Update() {
        Debug.Log(nav.velocity.sqrMagnitude);
        if (Input.GetKeyDown(KeyCode.F)) {
            target = true;
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            target = false;
        }
        if (target) {
            nav.destination = playerTransform.position;
        } else {
            nav.destination = returnTransform.position;
        }
    }*/
    /*
    public abstract void InAttackRange(bool enter);

    public abstract void InProjectileRange(bool enter);

    public abstract void InVisualRange(bool enter);*/
    protected virtual void Start() {
        playerTransform = CharacterManager.Instance.transform;
        characterHealth = CharacterManager.Instance.GetCharacterHealth();
        nav = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update() {
        attackCooldown -= Time.deltaTime;
        projectileCooldown -= Time.deltaTime;
        pause -= Time.deltaTime;
    }

    public void InAttackRange(bool enter) {
        inAttackRange = enter;
    }

    public void InProjectileRange(bool enter) {
        inProjectileRange = enter;
    }

    public void InVisualRange(bool enter) {
        inVisualRange = enter;
    }

    public void InDamageRange(bool enter) {
        inDamageRange = enter;
    }

    public void TriggerDeath() {
        Destroy(gameObject);
    }

    public virtual void ResetTriggerStates()
    {

    }


    public abstract void TriggerAttack();
    public abstract void TriggerProjectile();
    public abstract void TriggerDamage();
    public abstract void ProjectileHit(GameObject hit);
    public abstract void StartFollow();
}
