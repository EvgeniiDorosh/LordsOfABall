using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ObjectPooler))]
public class ObjectPoolerEditor : Editor {

	public override void OnInspectorGUI() {

		var script = target as ObjectPooler;

		script.size = EditorGUILayout.IntField ("Size", script.size);
		script.pooledType = (PooledType)EditorGUILayout.EnumPopup ("Type", script.pooledType);

		SerializedProperty targetObject = serializedObject.FindProperty ("targetObject");
		EditorGUILayout.PropertyField (targetObject);
		serializedObject.ApplyModifiedProperties ();

		SerializedProperty predefinedObjects = serializedObject.FindProperty ("predefinedObjects");

		if (script.targetObject == null) {			
			EditorGUILayout.PropertyField (predefinedObjects, true);
			serializedObject.ApplyModifiedProperties ();
			script.size = predefinedObjects.arraySize;
		} else {
			predefinedObjects.ClearArray ();
		}

		if (targetObject.objectReferenceValue == null && predefinedObjects.arraySize == 0) {
			Debug.LogWarning ("There is nothing to pool in " + script.pooledType.ToString ());
		}
	}
}
