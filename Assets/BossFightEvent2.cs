using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightEvent2 : MonoBehaviour
{
    [SerializeField] private GameObject activateBlock;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            activateBlock.SetActive(true);
            Destroy(gameObject);
        }
    }
}
