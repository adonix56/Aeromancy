using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private Image healthPanel;
    [SerializeField, Range(0f, 1f)] private float darkestRed;
    [SerializeField, Range(0f, 10f)] private float secondsToRecover;

    private float currentWait;
    private float alphaFade;
    private int currentHealth;
    private bool alive;

    private void Start() {
        alive = true;
        currentHealth = maxHealth;
    }

    private void Update() {
        if (alive) {
            if (currentWait > 0) {
                currentWait -= Time.deltaTime;
                alphaFade = darkestRed * (maxHealth - currentHealth) / (maxHealth - 1) * currentWait / secondsToRecover; //stupid equation but it works lol
                Color panelColor = healthPanel.color;
                panelColor.a = alphaFade;
                healthPanel.color = panelColor;
            } else {
                currentHealth = maxHealth;
            }
        }
    }

    public void GetHit() {
        if (--currentHealth == 0) {
            alive = false;
        }
        currentWait = secondsToRecover;
    }
}
