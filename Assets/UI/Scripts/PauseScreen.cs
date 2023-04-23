using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    private GameInput gameInput;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        gameInput = GameInput.Instance;
        gameInput.OnPauseAction += Pause;
    }

    void Pause(object sender, System.EventArgs e)
    {
        CharacterManager.Instance.SetPlayable(false);
        pauseScreen.SetActive(true);
    }

    void Resume(object sender, System.EventArgs e)
    {
        CharacterManager.Instance.SetPlayable(true);
        pauseScreen.SetActive(false);
    }
}
