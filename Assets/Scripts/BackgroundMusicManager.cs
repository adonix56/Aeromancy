using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public float fadeTime = 1.0f;
    private AudioClip currentBackgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<AudioSource>().clip = mainBackgroundMusic;
        //PlayMusic();
    }

    public void PlayMusic(AudioClip newBackgroundMusic)
    {
        if (currentBackgroundMusic == null)
        {
            GetComponent<AudioSource>().clip = newBackgroundMusic;
            GetComponent<AudioSource>().Play();
            currentBackgroundMusic = newBackgroundMusic;
        }
        else
        {
            LeanTween.value(1, 0, fadeTime).setOnUpdate(
                (float value) => {
                    GetComponent<AudioSource>().volume = value;
                }).setOnComplete(
                () => {
                    GetComponent<AudioSource>().volume = 1;
                    GetComponent<AudioSource>().clip = newBackgroundMusic;
                    GetComponent<AudioSource>().Play();
                    currentBackgroundMusic = newBackgroundMusic;
            });
        }
            
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }
}