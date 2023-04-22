using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInteraction : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform returnTransform;
    private NavMeshAgent nav;
    private bool target;

    private void Start() {
        playerTransform = CharacterManager.Instance.transform;
        nav = GetComponent<NavMeshAgent>();
        nav.enabled = false;
        nav.enabled = true;
        nav.Warp(transform.position);
    }

    private void Update() {
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
    }
}
