using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SecondFightEvent : MonoBehaviour {
    [SerializeField] private GameObject activateBlock;
    [SerializeField] private GameObject deactivateBlock;
    [SerializeField] private DialogTrigger[] followUpDialogues;
    [SerializeField] private CinemachineVirtualCamera[] cameras;
    [SerializeField] private SunflowerFairy[] fairies;
    [SerializeField] private SkillSO whirlWind;
    [SerializeField] private GameObject[] postActivateBlocks;
    [SerializeField] private GameObject postDeactivateBlock;
    [SerializeField] private CinemachineVirtualCamera postFightCamera;

    private bool closing;
    private BoxCollider thisCollider;

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
        foreach (SunflowerFairy fairy in fairies) {
            verify += fairy.IsAlive() ? 0 : 1;
        }
        return verify == fairies.Length;
    }


    public void ActivateDialogue(int index) {
        activateBlock.SetActive(true);
        followUpDialogues[index].StartDialogue();
        CameraTriggerController.Instance.TriggerEvent(cameras[0]);
    }

    public void GoBackToPlay() {
        thisCollider.enabled = false;
        CameraTriggerController.Instance.TriggerEvent(cameras[1]);
        CharacterManager.Instance.GetCharacterSkills().AddSkill(1, whirlWind);
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
