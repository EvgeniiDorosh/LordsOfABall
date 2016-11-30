using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PassiveDamageable))]
public class PassiveDamageableEditor : Editor {

	public override void OnInspectorGUI() {

		PrefabUtility.DisconnectPrefabInstance (target);

		var script = target as PassiveDamageable;

		script.isStable = EditorGUILayout.Toggle ("isStable", script.isStable);
		if (script.isStable) {
			script.level = (int)EditorGUILayout.Slider ("Level", script.level, 1, 200);
		}

		SerializedProperty hitSound = serializedObject.FindProperty ("hitSound");
		EditorGUILayout.PropertyField (hitSound);

		SerializedProperty death = serializedObject.FindProperty ("death");
		EditorGUILayout.PropertyField (death);

		serializedObject.ApplyModifiedProperties ();
	}
}
