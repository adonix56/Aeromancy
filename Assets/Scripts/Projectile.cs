using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseSkill
{
    private const string PLAYER = "Player";
    //private Vector3 direction;
    [SerializeField] private float travelSpeed;
    [SerializeField] private ParticleSystem trail;
    [SerializeField] private ParticleSystem impact;
    [SerializeField] private GameObject visualObject;

    private CharacterHealth characterHealth;
    private Vector3 direction;
    private bool dying = false;
    private float impactTime;
    private float waitToHit = 0.2f;

    private void Awake() {
        impactTime = impact.main.duration;
    }

    private void Start() {
        characterHealth = CharacterManager.Instance.GetCharacterHealth();
        direction = characterHealth.transform.position;
        direction.y += 1;
        transform.LookAt(direction);
    }

    private void OnTriggerEnter(Collider other) {
        if (VerifyHit(other)) {
            dying = true;
            trail.Stop();
            impact.gameObject.SetActive(true);
            if (visualObject) {
                visualObject.SetActive(false);
            }
            if (other.CompareTag(PLAYER)) {
                characterHealth.GetHit();
            }
        }
    }

    private bool VerifyHit(Collider other) {
        bool verify = false;
        if (dying) return false;
        if (waitToHit < 0) verify = true;
        if (other.CompareTag(PLAYER)) verify = true;
        if (GetComponent<Whirlwind>()) verify = true;
        return verify;
    }

    private void Update() {
        if (dying) {
            if (impactTime < 0) {
                Deactivate();
            }
            impactTime -= Time.deltaTime;
        } else {
            transform.position += transform.forward * Time.deltaTime * travelSpeed;
        }
        waitToHit -= Time.deltaTime;
    }

    public override void Activate() {
        throw new System.NotImplementedException();
    }

    public override void Deactivate() {
        Destroy(gameObject);
    }
}
