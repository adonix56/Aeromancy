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
            CharacterManager.Instance.SetPlayable(true);
        } 
        eventCounter++;
    }
}
