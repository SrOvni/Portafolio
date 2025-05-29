using UnityEditor;
using UnityEngine;

public static class GameObjectExtensions
{
    public static T SetActive<T>(this T obj) where T : MonoBehaviour
    {
        obj.gameObject.SetActive(true);
        return obj;
    }

    public static T SetInactive<T>(this T obj) where T : MonoBehaviour 
    {
        obj.gameObject.SetActive(false);
        return obj;
    }
}
