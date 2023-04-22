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

    private CharacterManager character; // TODO: Probably change to Character Health
    private Vector3 direction;
    private bool dying = false;

    private void Start() {
        character = CharacterManager.Instance; // TODO: .GetCharacterHealth();
        direction = character.transform.position;
        direction.y += 1;
        transform.LookAt(direction);
    }

    private void OnTriggerEnter(Collider other) {
        if (!dying) {
            dying = true;
            trail.Stop();
            explosion.gameObject.SetActive(true);
            if (other.CompareTag(PLAYER)) {
                Debug.Log("Ouch");
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
