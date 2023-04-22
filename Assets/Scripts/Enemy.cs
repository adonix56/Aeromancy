using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _returnTransform;
    [SerializeField] private Animator _animator;

    protected Transform playerTransform { get { return _playerTransform; } set { _playerTransform = value; } }
    protected Transform returnTransform { get { return _returnTransform; } set { _returnTransform = value; } }
    protected NavMeshAgent nav { get; set; }
    protected Animator animator { get { return _animator; } set { _animator = value; } }
    protected int health { get; set; }
    protected bool inVisualRange { get; set; }
    protected bool inAttackRange { get; set; }
    protected bool inProjectileRange { get; set; }
    protected bool inDamageRange { get; set; }

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

    public abstract void TriggerAttack();
    public abstract void TriggerProjectile();
    public abstract void ProjectileHit(GameObject hit);
    public abstract void StartFollow();
}
