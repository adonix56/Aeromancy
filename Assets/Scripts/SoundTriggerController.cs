using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggerController : MonoBehaviour {
    public AudioSource audioSource;
    public float fadeTime;
    private float targetVolume;
    void Update () {
        audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, (1.0f / fadeTime) * Time.deltaTime);
    }
    void OnTriggerEnter (Collider other) {
        if(other.CompareTag("Player")){
            targetVolume = 1.0f;
        }
    }
    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            targetVolume = 0.0f;
        }
    }
}