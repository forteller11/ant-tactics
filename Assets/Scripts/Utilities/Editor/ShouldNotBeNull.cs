using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Utilities.Editor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ShouldNotBeNull : PropertyAttribute
    {
        
    }

    [CustomPropertyDrawer(typeof(ShouldNotBeNull))]
    public class ShouldNotBeNullDrawer : PropertyDrawer
    {
        public static readonly Color WarningColor = new Color(1f, 0.56f, 0.3f);
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var reference = property.objectReferenceValue;
            if (property.propertyType != SerializedPropertyType.ExposedReference &&
                property.propertyType != SerializedPropertyType.ManagedReference && 
                property.propertyType != SerializedPropertyType.ObjectReference)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }
        
            if (reference != null)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }
            
            Color colorTemp = GUI.color;
            GUI.color = WarningColor;
            label.text = $"(Should Not Be Null) {label.text}";
            EditorGUI.PropertyField(position, property, label);
            GUI.color = colorTemp;
        }
        
    }
}