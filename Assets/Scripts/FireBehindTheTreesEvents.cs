using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FireBehindTheTreesEvents : MonoBehaviour
{
    private const string WAITING = "Waiting";
    private const string MOVING = "Moving";

    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private Animator characterAnimator;

    public void Play()
    {
        CharacterManager.Instance.SetPlayable(false);
        characterAnimator.SetFloat(MOVING, 0);
        timeline.stopped += Stop;
        timeline.Play();
    }

    private void Stop(PlayableDirector timeline)
    {
        CharacterManager.Instance.SetPlayable(true);
        //characterAnimator.SetTrigger(WAITING);
    }
}
