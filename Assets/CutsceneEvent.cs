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

    private void OnEnable() {
        if (eventCounter == 0) {
            Debug.Log("event1");
            characterAnimator.SetTrigger(WAITING);
        } else if (eventCounter == 1) {
            Debug.Log("event3");
            characterAnimator.SetTrigger(WAITING);
        }
    }

    private void OnDisable() {
        if (eventCounter == 0) {
            Debug.Log("event2");
        } else if (eventCounter == 1) {
            Debug.Log("event4");
            meditationParticle.Stop(true);
        }
        eventCounter++;
    }
}
