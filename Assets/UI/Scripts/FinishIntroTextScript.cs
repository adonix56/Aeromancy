using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishIntroTextScript : MonoBehaviour
{
    public Image blackOverlay;
    public float fadeOutTime = 1.0f;
    public GameLoader gameLoader;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<DialogPanel>().OnCloseEvent += CloseIntro;
    }

    private void CloseIntro()
    {
        Color color = blackOverlay.color;
        color.a = 0;
        blackOverlay.color = color;
        blackOverlay.gameObject.SetActive(true);

        LeanTween.value(0, 1, fadeOutTime).
            setOnUpdate((float value) => {
                color.a = value;
                blackOverlay.color = color;
            }).
            setOnComplete(
            () => {
                gameLoader.StartGame();
            });
    }
}
