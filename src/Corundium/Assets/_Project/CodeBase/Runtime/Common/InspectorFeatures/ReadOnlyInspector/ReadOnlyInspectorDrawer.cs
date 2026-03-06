using UnityEditor;
using UnityEngine;

namespace DevFuckers._Project.CodeBase.Runtime.Common.InspectorFeatures.ReadOnlyInspector
{
    [CustomPropertyDrawer(typeof(ReadOnlyInspectorAttribute))]
    public class ReadOnlyInspectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;

            EditorGUI.PropertyField(position, property, label);

            GUI.enabled = true;
        }
    }
}