using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Ending : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;
    private void OnTriggerEnter(Collider other) {
        CharacterManager.Instance.SetPlayable(false);
        cutscene.Play();
    }
}
