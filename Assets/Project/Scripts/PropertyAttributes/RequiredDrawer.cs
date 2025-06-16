#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Data.Common;

[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        EditorGUI.PropertyField(position, property, label);

        bool isInvalid = false;

        isInvalid = property.propertyType switch
        {
            SerializedPropertyType.ObjectReference => property.objectReferenceValue == null,
            SerializedPropertyType.String => string.IsNullOrEmpty(property.stringValue),
            SerializedPropertyType.Generic => property.isArray ? property.arraySize == 0 : true
        };

        if (isInvalid)
        {
            var attr = attribute as RequiredAttribute;
            string message = string.IsNullOrEmpty(attr.Message)
            ? $"{property.displayName} is required"
            :attr.Message;
            var helpBoxRect = new Rect(position.x, position.yMax + 2, position.width, EditorGUIUtility.singleLineHeight * 2);
            EditorGUI.HelpBox(helpBoxRect, message, MessageType.Error);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        bool isInvalid = property.propertyType switch
        {
            SerializedPropertyType.ObjectReference => property.objectReferenceValue == null,
            SerializedPropertyType.String => string.IsNullOrEmpty(property.stringValue),
            SerializedPropertyType.Generic => property.isArray ? property.arraySize == 0 : false,
            _ => true,
        };

        float baseHeight = EditorGUI.GetPropertyHeight(property, label, true);
        return isInvalid ? baseHeight + EditorGUIUtility.singleLineHeight * 2 + 2 : baseHeight;
        
    }
}
#endif