using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CutsceneEvent : MonoBehaviour
{
    private const string WAITING = "Waiting";
    [SerializeField] private int eventCounter = 0;

    //Opening Cutscene
    [SerializeField] private ParticleSystem meditationParticle;
    [SerializeField] private Image fadeBlack;
    [SerializeField] private CinemachineVirtualCamera outsideCamera;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private GameObject cutsceneWizard;
    [SerializeField] private GameObject playerWizard;

    private void OnEnable() {
        if (eventCounter == 0) {
            // Event 1
            GameManager.Instance.gameUI.SetActive(false);
            CharacterManager.Instance.SetPlayable(false);
            characterAnimator.SetTrigger(WAITING);
        } else if (eventCounter == 1) {
            // Event 3
            meditationParticle.Stop(true);
        } 
    }

    private void OnDisable() {
        if (eventCounter == 0) {
            // Event 2 - Non-event
            characterAnimator.SetTrigger(WAITING);
        } else if (eventCounter == 1) {
            // Event 4
            GameManager.Instance.gameUI.SetActive(true);
            CharacterManager.Instance.SetPlayable(true);

            string[] texts = new string[] { "Something is off... Breathing feels... Powerful!"};
            DialogPanel panel = GameManager.Instance.gameUI.GetComponent<DialogController>().OpenBaseDialogPanel(texts, true);
            panel.OnCloseEvent += () => {
                string[] texts = new string[] { "Use W/A/S/D to move." };
                GameManager.Instance.gameUI.GetComponent<DialogController>().OpenThinUpRightPanel(texts, false);
            };
        } 
        eventCounter++;
    }
}
