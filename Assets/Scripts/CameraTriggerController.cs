using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggerController : MonoBehaviour
{
    public static CameraTriggerController Instance { get; private set; }

    private CinemachineVirtualCamera currentCamera;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Trigger Controller created.");
        }
        Instance = this;
    }

    private void Start() {
        currentCamera = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
    }

    public void TriggerEvent(CinemachineVirtualCamera camera) {
        camera.Priority = 11;
        currentCamera.Priority = 10;
        currentCamera = camera;
    }

    public CinemachineVirtualCamera getCurrentCamera() {
        return currentCamera;
    }
}
