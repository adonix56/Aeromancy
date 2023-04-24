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
        ResetAllEnemiesTriggerStates();
        controller.transform.position = spawnManager.CurrentSpawnLocation();
        controller.enabled = true;
        //Physics.SyncTransforms();

        GetComponent<CharacterBreathLevel>().RestoreEnergy();
        GetComponent<CharacterHealth>().Restore();
        GetComponent<FogReaction>().RestartFogCount();
        GameManager.Instance.GetComponent<EnvironmentManager>().ChangeToDefaultEnvironment();
        transform.Rotate(0, 0, 0);
    }

    private void ResetAllEnemiesTriggerStates() {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies) {
            enemy.ResetTriggerStates();
        }
    }
}
