using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanelController : MonoBehaviour
{
    public Sprite lifeSprite;
    public Sprite consumedLifeSprite;

    public void SetLives(int lives)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (lives > 0)
                transform.GetChild(i).GetComponent<Image>().sprite = lifeSprite;
            else
                transform.GetChild(i).GetComponent<Image>().sprite = consumedLifeSprite;
            lives--;
        }
    }
}
