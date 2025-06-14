using UnityEngine;

public static class ExtendsColors
{
    public static Tween FadeTo(this Material material, Color flashColor, float fadeDuration)
    {
        Color startColor = material.color;
        Tween tween = new Tween(
            fadeDuration,
            progress => 
            {
                material.color = Color.Lerp(startColor, flashColor, progress);
            }
            
        );
        return tween;

    }

    public static Tween FadeTo(this Material material, float fadeValue, float fadeDuration)
    {
        float initialAlpha = material.color.a;
        float alpha;
        Tween tween = new Tween(
            fadeDuration,
            progress => 
            {
                alpha = Mathf.Lerp(initialAlpha, fadeValue, progress);
                material.color = new Color(material.color.r, material.color.g, material.color.b, alpha);
            }
        );
        return tween;
    }
}
