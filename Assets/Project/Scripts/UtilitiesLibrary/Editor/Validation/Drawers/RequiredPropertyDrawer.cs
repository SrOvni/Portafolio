using UnityEngine;
using UnityEditor;
using UtilitiesLibrary.Validation.Attributes;
using System.Collections.Generic;
namespace UtilitiesLibrary.Validation.Drawer
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredPropertyDrawer : PropertyDrawer
    {
        HashSet<string> loggedMessages;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var requiredAttribute = (RequiredAttribute)attribute;

            var isSupportedProperty = PropertyValidation.IsSupportedType(property);
            if (!isSupportedProperty)
            {
                EditorGUI.HelpBox(position, "[Required] Can only be used on reference types or string.", MessageType.Warning);
                return;
            }

            Rect fieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.PropertyField(fieldRect, property, label);

            bool isEmpty = PropertyValidation.IsAnEmptyProperty(property);

            if (isEmpty)
            {
                if (requiredAttribute.LogToConsoleIfNullOrEmpty)
                {
                    var origin = property.serializedObject.targetObject;
                    Debug.Log($"{property.name} field in {origin}");
                }
                Rect helpBox = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight * 1.5f);

                EditorGUI.HelpBox(helpBox, requiredAttribute.Message, MessageType.Warning);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            bool isSupportedType = PropertyValidation.IsSupportedType(property);
            if (!isSupportedType)
                return EditorGUIUtility.singleLineHeight * 2f;

            bool isEmpty = PropertyValidation.IsAnEmptyProperty(property);

            float baseHeight = EditorGUIUtility.singleLineHeight;
            return isEmpty ? EditorGUIUtility.singleLineHeight * 2f + 2 : baseHeight;

        }


    }
}