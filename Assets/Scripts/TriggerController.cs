using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;

    private CinemachineVirtualCamera currentCamera;

    private void Start() {
    }

    public enum TriggerEvents { 
        Follow, SideCamera1
    }

    public void TriggerEvent(TriggerController.TriggerEvents triggerEvent) { 
        switch (triggerEvent) {
            case TriggerEvents.Follow:

                break;
            case TriggerEvents.SideCamera1:
                break;
        }
    }
}
