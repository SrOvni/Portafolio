using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
namespace UtilitiesLibrary.Validation
{

    [CustomPropertyDrawer(typeof(RequiredFieldAttribute))]
    public class RequiredFieldDrawer : PropertyDrawer
    {

        private void Awake()
        {
            Button button = new GameObject("Button").AddComponent<Button>();
            button.onClick.AddListener(() => Debug.Log("Hola mundo"));


        }


    }
}
