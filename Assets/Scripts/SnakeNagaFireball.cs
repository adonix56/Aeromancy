using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeNagaFireball : BaseSkill
{
    private const string PLAYER = "Player";
    //private Vector3 direction;
    [SerializeField] private float travelSpeed;
    [SerializeField] private ParticleSystem trail;
    [SerializeField] private ParticleSystem explosion;

    private CharacterHealth characterHealth;
    private Vector3 direction;
    private bool dying = false;

    private void Start() {
        characterHealth = CharacterManager.Instance.GetCharacterHealth();
        direction = characterHealth.transform.position;
        direction.y += 1;
        transform.LookAt(direction);
    }

    private void OnTriggerEnter(Collider other) {
        if (!dying) {
            dying = true;
            trail.Stop();
            explosion.gameObject.SetActive(true);
            if (other.CompareTag(PLAYER)) {
                characterHealth.GetHit();
            }
        }
    }

    private void Update() {
        if (dying) {
            if (explosion == null) {
                Deactivate();
            }
        } else {
            transform.position += transform.forward * Time.deltaTime * travelSpeed;
        }
    }

    public override void Activate() {
        throw new System.NotImplementedException();
    }

    public override void Deactivate() {
        Destroy(this);
    }
}
