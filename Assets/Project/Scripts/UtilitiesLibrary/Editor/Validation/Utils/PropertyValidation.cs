using System.Reflection;
using UnityEditor;
using UnityEngine;
namespace UtilitiesLibrary.Validation
{
    public static class PropertyValidation
    {
        public static bool IsAnEmptyProperty(SerializedProperty property) =>
            property.propertyType switch
            {
                SerializedPropertyType.ObjectReference => property.objectReferenceValue == null,
                SerializedPropertyType.String => string.IsNullOrWhiteSpace(property.stringValue),
                SerializedPropertyType.Generic => (property.isArray && property.propertyPath != "m_Script") ? property.arraySize == 0 : false,
                SerializedPropertyType.Color => property.colorValue == default,
                SerializedPropertyType.Vector2 => property.vector2Value == Vector2.zero,
                SerializedPropertyType.Vector3 => property.vector3Value == Vector3.zero,
                _ => false
            };
        public static bool IsSupportedType(SerializedProperty property)
        {
            return property.propertyType is SerializedPropertyType.ObjectReference || property.propertyType is SerializedPropertyType.String || property.isArray;
        }

        public static bool IsNotZero(SerializedProperty property) =>

                property.propertyType switch
                {
                    SerializedPropertyType.Integer => property.intValue != 0,
                    SerializedPropertyType.Float => property.floatValue != 0f,
                    _ => false,
                };
    }
}