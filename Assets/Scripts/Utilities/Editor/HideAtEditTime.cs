using System;
using UnityEditor;
using UnityEngine;

namespace Utilities.Editor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class HideAtEditTime : PropertyAttribute
    {
        
    }

    [CustomPropertyDrawer(typeof(HideAtEditTime))]
    public class HideAtEditTimeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!Application.isPlaying)
                return;

            EditorGUI.PropertyField(position, property, label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!Application.isPlaying)
                return 0f;

            return base.GetPropertyHeight(property, label);
        }
    }
}