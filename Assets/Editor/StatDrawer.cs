using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Stat))]
public class StatDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float labelWidth = 50;
        float fieldWidth = (position.width - labelWidth * 2) / 2;

        Rect currentLabelRect = new Rect(position.x, position.y, labelWidth, position.height);
        Rect currentFieldRect = new Rect(position.x + labelWidth, position.y, fieldWidth, position.height);

        Rect maxLabelRect = new Rect(position.x + labelWidth + fieldWidth + 5, position.y, labelWidth, position.height);
        Rect maxFieldRect = new Rect(position.x + labelWidth * 2 + fieldWidth + 5, position.y, fieldWidth, position.height);

        SerializedProperty currentProp = property.FindPropertyRelative("current");
        SerializedProperty maxProp = property.FindPropertyRelative("max");

        if (currentProp != null && maxProp != null)
        {
            EditorGUI.LabelField(currentLabelRect, new GUIContent("Current"));
            EditorGUI.PropertyField(currentFieldRect, currentProp, GUIContent.none);

            EditorGUI.LabelField(maxLabelRect, new GUIContent("Max"));
            EditorGUI.PropertyField(maxFieldRect, maxProp, GUIContent.none);
        }
        else
        {
            Debug.LogError("Could not find 'current' or 'max' properties in Stat.");
        }

        EditorGUI.indentLevel = indent;
        EditorGUI.EndProperty();
    }
}
