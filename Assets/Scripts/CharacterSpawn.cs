using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawn : MonoBehaviour
{
    private SpawnManager spawnManager;
    private CharacterController controller;
    private int fallLimit = -20;

    private void Start() {
        spawnManager = SpawnManager.Instance;
        controller = CharacterManager.Instance.GetCharacterController();
    }

    private void Update() {
        CheckDeath();
    }

    private void CheckDeath() {
        if (controller.transform.position.y <= fallLimit) {
            Respawn();
        }
    }

    public void Respawn()
    {
        controller.enabled = false;
        controller.transform.position = spawnManager.CurrentSpawnLocation();
        controller.enabled = true;
        GetComponent<CharacterBreathLevel>().RestoreEnergy();
        GetComponent<CharacterHealth>().Restore();
        transform.Rotate(0, 0, 0);
    }
}
