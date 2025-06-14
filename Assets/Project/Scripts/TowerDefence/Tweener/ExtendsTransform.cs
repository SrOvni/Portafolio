using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendsTransform
{
    public static Tween ScaleTo(this Transform transform, Vector3 vector3, float duration)
    {
        Tween tween= new Tween(
            duration,
            progress => {
                transform.localScale = Vector3.Lerp(transform.localScale, vector3, progress);
            }
        );
        return tween;
    }
}
