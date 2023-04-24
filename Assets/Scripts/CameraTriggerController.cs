using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTriggerController : MonoBehaviour
{
    public static CameraTriggerController Instance { get; private set; }
    [SerializeField] private CinemachineVirtualCamera defaultCamera;

    private CinemachineVirtualCamera currentCamera;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Trigger Controller created.");
        }
        Instance = this;
    }

    private void Start() {
        if (Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera == null)
        {
            currentCamera = defaultCamera;
        }
        else
        {
            currentCamera = Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        }
    }

    public void TriggerEvent(CinemachineVirtualCamera camera) {
        //camera.Priority = currentCamera.Priority + 1;
        Debug.Log($"{currentCamera.name} change to {camera.name}");
        camera.Priority = 10;
        currentCamera.Priority = 0;
        currentCamera = camera;
    }

    public CinemachineVirtualCamera getCurrentCamera() {
        return currentCamera;
    }
}
