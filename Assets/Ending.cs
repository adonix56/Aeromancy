using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;
    private bool isPlaying;


    private void Update() {
        if (isPlaying) {
            if (cutscene.state == PlayState.Paused)
                SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerEnter(Collider other) {
        CharacterManager.Instance.SetPlayable(false);
        cutscene.Play();
    }
}
