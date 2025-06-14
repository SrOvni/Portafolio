using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ExtendsMaterials
{
    public static IEnumerator FadeToRoutine(Material material, float value, float animationDurationInSeconds, AnimationDelegates animationDelegates)
    {
        float elapsedTime = 0;
        Material initialMaterial = material;
        float initialAlpha = material.color.a;
        while(elapsedTime < animationDurationInSeconds)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime/animationDurationInSeconds);
            float alpha = Mathf.Lerp(initialAlpha, value, t); 
            material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
            yield return null;
        }
        //material.color = new Color(material.color.r, material.color.g, material.color.b, value);
        animationDelegates.OnComplete();
    }
    public static AnimationDelegates FadeTo(this Material material, float value, float animationDurationInSeconds, MonoBehaviour host)
    {
        AnimationDelegates animationDelegates = new AnimationDelegates();
        host.StartCoroutine(FadeToRoutine(material, value, animationDurationInSeconds, animationDelegates));
        return animationDelegates;
    }
    
}

