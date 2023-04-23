using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public AudioClip mainBackgroundMusic;
    public AudioClip otherBackgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().clip = mainBackgroundMusic;
        PlayMusic();
    }

    public void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
    }
}