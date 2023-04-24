using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ForestSpiritEvent : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera changeToCamera;
    [SerializeField] private GameObject forestSpirit;

    private CinemachineVirtualCamera lastCamera;

    public void Play()
    {
        lastCamera = CameraTriggerController.Instance.getCurrentCamera();
        CameraTriggerController.Instance.TriggerEvent(changeToCamera);
        forestSpirit.SetActive(true);
    }

    public void Finish()
    {
        CameraTriggerController.Instance.TriggerEvent(lastCamera);
        forestSpirit.SetActive(false);
        Destroy(gameObject);
    }
}
