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
            
            base.OnGUI(position, property, label);
        }
    }
}