using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using System;

[CanEditMultipleObjects]
[CustomEditor(typeof(Laser))]
public class LaserEditor : Editor {

	public override void OnInspectorGUI() {
		
		string[] LayerNames = GetSortingLayerNames();
		SerializedProperty SortingLayer = serializedObject.FindProperty("sortingLayer");

		serializedObject.Update();

		Rect firstHoriz = EditorGUILayout.BeginHorizontal();
		EditorGUI.BeginChangeCheck();
		EditorGUI.BeginProperty(firstHoriz, GUIContent.none, SortingLayer);
		int layerId = 0;

		for (int i = 0; i < LayerNames.Length; i++)
			if (SortingLayer.stringValue == LayerNames[i])
				layerId = i;

		layerId = EditorGUILayout.Popup("Sorting Layer", layerId, LayerNames, EditorStyles.popup);
		SortingLayer.stringValue = LayerNames[layerId];
		EditorGUI.EndProperty();
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.PropertyField(serializedObject.FindProperty("orderLayer"), true);

		EditorGUILayout.PropertyField(serializedObject.FindProperty("mainPart"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("glowPart"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("color"), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("glowColor"), true);

		serializedObject.ApplyModifiedProperties();
	}

	public string[] GetSortingLayerNames() {
		Type internalEditorUtilityType = typeof(InternalEditorUtility);
		PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
		return (string[])sortingLayersProperty.GetValue(null, new object[0]);
	}
}
