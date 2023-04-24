using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstFightEvent : MonoBehaviour
{
    [SerializeField] private GameObject activateBlock;
    [SerializeField] private DialogTrigger followUpDialogue;
    [SerializeField] private CinemachineVirtualCamera flytrapCamera;
    [SerializeField] private WolfPup[] wolfPups;
    [SerializeField] private GameObject[] postActivateBlocks;
    [SerializeField] private GameObject postDeactivateBlock;
    [SerializeField] private CinemachineVirtualCamera postFightCamera;

    private CinemachineVirtualCamera lastCamera;
    private BoxCollider thisCollider;
    private bool closing;

    private void Start() {
        thisCollider = GetComponent<BoxCollider>();
    }

    private void Update() {
        if (VerifyFightEnd() && !closing) {
            closing = true;
            EndFight();
        }
    }

    private bool VerifyFightEnd() {
        int verify = 0;
        foreach (WolfPup pup in wolfPups) {
            verify += pup.IsAlive() ? 0 : 1;
        }
        return verify == wolfPups.Length;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            CharacterManager.Instance.GetCharacterAnimation().Move(0);
            activateBlock.SetActive(true);
            lastCamera = CameraTriggerController.Instance.getCurrentCamera();
        }
    }

    public void ActivateDialogue() {
        CameraTriggerController.Instance.TriggerEvent(flytrapCamera);
        followUpDialogue.StartDialogue();
    }

    public void GoBackToPlay() {
        CameraTriggerController.Instance.TriggerEvent(lastCamera);
        thisCollider.enabled = false;
    }

    public void EndFight() {
        CameraTriggerController.Instance.TriggerEvent(postFightCamera);
        foreach (GameObject obj in postActivateBlocks) {
            obj.SetActive(true);
        }
        postDeactivateBlock.SetActive(false);
        Destroy(gameObject);
    }
}
