using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggerEvent : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField] private CinemachineVirtualCamera changeToCamera;
    [SerializeField] private bool updateSpawn;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(PLAYER_TAG)) {
            CameraTriggerController.Instance.TriggerEvent(changeToCamera);
            if (updateSpawn) {
                updateSpawn = false;
                SpawnManager.Instance.DequeueNextSpawnLocation();
            }
        }
    }
}
