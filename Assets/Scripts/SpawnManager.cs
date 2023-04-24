using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }
    //[SerializeField] private Spawn[] spawnLocations;
    [SerializeField] private Queue<Spawn> spawnLocations;

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
        }
        Instance = this;
        spawnLocations = new Queue<Spawn>();
    }

    private void Start() {
        foreach (Transform child in transform) {
            QueueSpawnLocation(child.GetComponent<Spawn>());
        }
    }

    public void QueueSpawnLocation(Spawn spawnLocation) {
        spawnLocations.Enqueue(spawnLocation);
    }

    public Vector3 CurrentSpawnLocation() {
        return spawnLocations.Peek().transform.position;
    }

    public CinemachineVirtualCamera CurrentSpawnCamera() {
        return spawnLocations.Peek().spawnCamera;
    }

    public Spawn DequeueNextSpawnLocation() {
        return spawnLocations.Dequeue();
    }

    public int LengthSpawnLocations() {
        return spawnLocations.Count;
    }
}
