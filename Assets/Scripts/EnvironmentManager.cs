using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public Material defaultSkybox;
    public Color defaultSkyColor;
    public Material fogSkybox;
    public Color fogColor;
    public float fogDensity;
    public float transitionTime;

    //public bool fogTrigger;

    enum EnvironmentType { Default, Fog };
    private EnvironmentType currentEnvironment;
    private LTDescr currentLT;

    // Start is called before the first frame update
    void Start()
    {
        currentEnvironment = EnvironmentType.Default;
        RenderSettings.fog = false;
        RenderSettings.fogDensity = 0;
        fogSkybox.SetColor("_Tint", defaultSkyColor);
    }

    //private void Update()
    //{
    //    if(fogTrigger)
    //    {
    //        ChangeToFogEnvironment();
    //    } else
    //    {
    //        ChangeToDefaultEnvironment();
    //    }
    //}

    public void ChangeToFogEnvironment()
    {
        if (currentEnvironment == EnvironmentType.Default)
        {
            currentEnvironment = EnvironmentType.Fog;
            RenderSettings.skybox = fogSkybox;
            RenderSettings.fog = true;
            float currentDensity = RenderSettings.fogDensity;
            Color currentColor = fogSkybox.GetColor("_Tint");

            if (currentLT != null)
                LeanTween.cancel(currentLT.id);
            currentLT = LeanTween.value(0, 1, transitionTime).setEaseInQuad().setOnUpdate((float value) => {
                RenderSettings.fogDensity = Mathf.Lerp(currentDensity, fogDensity, value);
                fogSkybox.SetColor("_Tint", Vector4.Lerp(currentColor, fogColor, value));
            });
        }
    }

    public void ChangeToDefaultEnvironment()
    {
        if (currentEnvironment == EnvironmentType.Fog)
        {
            currentEnvironment = EnvironmentType.Default;
            float currentDensity = RenderSettings.fogDensity;
            Color currentColor = fogSkybox.GetColor("_Tint");

            if (currentLT != null)
                LeanTween.cancel(currentLT.id);
            currentLT = LeanTween.value(1, 0, transitionTime).setEaseInQuad().setOnUpdate((float value) => {
                RenderSettings.fogDensity = Mathf.Lerp(0, currentDensity, value);
                fogSkybox.SetColor("_Tint", Vector4.Lerp(defaultSkyColor, currentColor, value));
            }).setOnComplete(() => {
                RenderSettings.skybox = defaultSkybox;
                RenderSettings.fog = false;
                RenderSettings.fogDensity = 0;
            });
        }
    }
}
