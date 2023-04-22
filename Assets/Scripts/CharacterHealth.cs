using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int maxLivesNumber;
    [SerializeField] private HealthPanelController healthPanel;
    [SerializeField] private Image hitEffectOverlay;
    [SerializeField, Range(0f, 1f)] private float darkestAlpha;
    [SerializeField] float hitBufferTime = 1.0f;

    private int currentLives;
    private bool alive;
    private bool hitable;

    // Interaction with fire
    private Burnable burnable;

    private void Start() {
        alive = true;
        hitable = true;
        currentLives = maxLivesNumber;
        burnable = GetComponent<Burnable>();
        burnable.OnBurnEnter += GetHit;
    }

    //private void Update() {
    //    if (alive) {
    //        if (currentWait > 0) {
    //            currentWait -= Time.deltaTime;
    //            alphaFade = darkestRed * (maxHealth - currentHealth) / (maxHealth - 1) * currentWait / secondsToRecover; //stupid equation but it works lol
    //            Color panelColor = healthPanel.color;
    //            panelColor.a = alphaFade;
    //            healthPanel.color = panelColor;
    //        } else {
    //            currentHealth = maxHealth;
    //        }
    //    }
    //}

    public void GetHit() {
        if (!hitable) return;
        if (--currentLives == 0) {
            alive = false;
        }
        healthPanel.SetLives(currentLives);
        TriggerHitEffect();
        //currentWait = secondsToRecover;
    }

    public void TriggerHitEffect()
    {
        hitable = false;
        Color currentColor = hitEffectOverlay.color;
        currentColor.a = darkestAlpha;
        LeanTween.value(darkestAlpha, 0, hitBufferTime).setOnUpdate(
            (float value) => {
                currentColor.a = value;
                hitEffectOverlay.color = currentColor;
            }).setOnComplete(
            () => {
                hitable = true;
            });
    }
}
