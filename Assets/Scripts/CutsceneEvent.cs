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
    [SerializeField] private GameObject gameUI;
    [SerializeField] private AudioClip mainTheme;
    //[SerializeField] private GameObject cutsceneWizard;
    //[SerializeField] private GameObject playerWizard;

    private void OnEnable() {
        if (eventCounter == 0) {
            // Event 1 - Opening Cutscene
            gameUI.SetActive(false);
            CharacterManager.Instance.SetPlayable(false);
            CharacterManager.Instance.SetInCutscene(true);
            characterAnimator.SetTrigger(WAITING);
        } else if (eventCounter == 1) {
            // Event 3 - Opening Cutscene
            meditationParticle.Stop(true);
        } else if (eventCounter == 2) {
            // Event 5
            CharacterManager.Instance.SetPlayable(false);
            CharacterManager.Instance.SetInCutscene(true);
        }
    }

    private void OnDisable() {
        if (eventCounter == 0) {
            // Event 2 - Non-event
            characterAnimator.SetTrigger(WAITING);
        } else if (eventCounter == 1) {
            // Event 4
            gameUI.SetActive(true);
            CharacterManager.Instance.SetPlayable(true);
            CharacterManager.Instance.SetInCutscene(false);

            string[] texts = new string[] { "Something is off... Breathing feels... Powerful!"};
            DialogPanel panel = gameUI.GetComponent<DialogController>().OpenBaseDialogPanel(texts, true);
            panel.OnCloseEvent += () => {
                string[] texts = new string[] { "Use W/A/S/D to move." };
                gameUI.GetComponent<DialogController>().OpenThinUpRightPanel(texts, false);
            };
            // GameManager.Instance.GetComponent<BackgroundMusicManager>().PlayMusic(mainTheme);
        } else if (eventCounter == 2) {
            // Event 6
            CharacterManager.Instance.SetPlayable(true);
            CharacterManager.Instance.SetInCutscene(false);
        }
        eventCounter++;
    }
}
