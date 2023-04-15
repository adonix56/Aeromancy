using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggerEvent : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField] private CinemachineVirtualCamera changeToCamera;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(PLAYER_TAG)) {
            TriggerController.Instance.TriggerEvent(changeToCamera);
        }
    }
}
