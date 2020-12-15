﻿using Schwer.ItemSystem;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Inventory))]
public class InventoryDrawer : PropertyDrawer {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        var keys = property.FindPropertyRelative("keys");
        var values = property.FindPropertyRelative("values");

        if (keys.arraySize != values.arraySize) {
            var warning = "The number of keys does not match the number of values!";
            var details = $"({keys.arraySize} keys; {values.arraySize} values)";
            EditorGUILayout.HelpBox(warning + "\n" + details, MessageType.Warning);
        }

        property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, "Contents (" + keys.arraySize + ")", true);

        if (property.isExpanded) {
            EditorGUI.BeginDisabledGroup(true);
            for (int i = 0; i < keys.arraySize; i++) {
                EditorGUILayout.BeginHorizontal();
                var key = keys.GetArrayElementAtIndex(i);
                var value = values.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(key, GUIContent.none);
                EditorGUILayout.PropertyField(value, GUIContent.none);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}
