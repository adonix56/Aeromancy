using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenusFlytrap : MonoBehaviour
{
    //private const string WOLFPUP = "Wolfpup";

    [SerializeField] private Transform enemyHoldPosition;
    [SerializeField] private GameObject pointLight;
    [SerializeField] private Animator animator;

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
}
