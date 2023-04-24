using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ForestSpiritEvent : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera changeToCamera;
    [SerializeField] private GameObject forestSpirit;

    private CinemachineVirtualCamera lastCamera;
    private bool play;

    public void Play()
    {
        if (!play) { // Sometimes, Play() gets called twice, making the camera not come back
            lastCamera = CameraTriggerController.Instance.getCurrentCamera();
            CameraTriggerController.Instance.TriggerEvent(changeToCamera);
            forestSpirit.SetActive(true);
            play = true;
        }
    }

    public void Finish()
    {
        CameraTriggerController.Instance.TriggerEvent(lastCamera);
        forestSpirit.GetComponent<ForestSpirit>().Disappear();
        Destroy(gameObject);
    }
}
