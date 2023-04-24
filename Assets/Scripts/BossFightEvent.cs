using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class BossFightEvent : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;
    [SerializeField] private GameObject activateBlock;
    [SerializeField] private GameObject deactivateBlock;
    [SerializeField] private DialogTrigger[] followUpDialogues;
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private SnakeNaga naga;
    [SerializeField] private GameObject spirit;
    [SerializeField] private GameObject[] postActivateBlocks;
    [SerializeField] private GameObject[] postDeactivateBlocks;
    [SerializeField] private CinemachineVirtualCamera postFightCamera;

    private bool closing;
    private BoxCollider thisCollider;
    private bool cutscenePlayed;
    private bool cutsceneEnded;

    private void Start() {
        thisCollider = GetComponent<BoxCollider>();
    }

    private void Update() {
        if (cutsceneEnded) {
            if (VerifyFightEnd() && !closing) {
                closing = true;
                EndFight();
            }
        } else if (cutscenePlayed) {
            if (cutscene.state == PlayState.Paused) {
                cutsceneEnded = true;
                ActivateDialogue(0);
            }
        }
    }

    private bool VerifyFightEnd() {
        return !naga.IsAlive();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            cutscene.Play();
            cutscenePlayed = true;
            CharacterManager.Instance.GetCharacterAnimation().Move(0);
            activateBlock.SetActive(true);
            //deactivateBlock.SetActive(false);
            //lastCamera = CameraTriggerController.Instance.getCurrentCamera();
        }
    }

    public void ActivateDialogue(int index) {
        activateBlock.SetActive(true);
        followUpDialogues[index].StartDialogue();
        CameraTriggerController.Instance.TriggerEvent(cameras[0]);
    }

    public void GoBackToPlay() {
        thisCollider.enabled = false;
        spirit.SetActive(false);
        CameraTriggerController.Instance.TriggerEvent(cameras[1]);
    }

    public void EndFight() {
        CameraTriggerController.Instance.TriggerEvent(postFightCamera);
        foreach (GameObject obj in postActivateBlocks) {
            obj.SetActive(true);
        }
        foreach (GameObject obj in postDeactivateBlocks) {
            obj.SetActive(false);
        }
        Destroy(gameObject);
    }
}
