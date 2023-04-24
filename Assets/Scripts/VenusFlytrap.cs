using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusFlytrap : MonoBehaviour
{
    private const string SNAKE_NAGA = "Snake Naga";
    private const string DIE = "Die";

    [SerializeField] private Transform enemyHoldPosition;
    [SerializeField] private GameObject pointLight;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject explosion;

    private bool hasEaten;

    private void Start() {
    }

    public void Eat(Enemy enemy, string enemyName) {
        hasEaten = true;
        enemy.transform.parent = enemyHoldPosition;
        pointLight.SetActive(false);
        animator.SetTrigger(enemyName);
    }

    public bool HasEaten() {
        return hasEaten;
    }

    private void OnTriggerEnter(Collider other) {
        if (!hasEaten) {
            NagaConnector nagaConnector = other.GetComponent<NagaConnector>();
            if (nagaConnector) {
                SnakeNaga naga = nagaConnector.GetSnakeNaga();
                if (naga.IsInWhirlwind()) {
                    naga.GetEaten(this);
                    Eat(naga, SNAKE_NAGA);
                }
            }
        }
    }

    public void Explode() {
        explosion.SetActive(true);
        animator.SetTrigger(DIE);
    }
}
