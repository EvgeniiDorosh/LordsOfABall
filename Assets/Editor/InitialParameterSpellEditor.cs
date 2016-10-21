using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(InitialParameterSpell))]
public class InitialParameterSpellEditor : Editor {

	float minLimit = 0;
	float maxLimit = 100f;

	public override void OnInspectorGUI() {

		var script = target as InitialParameterSpell;

		script.itemName = EditorGUILayout.TextField ("Spell Name", script.itemName);
		script.parameterName = (InitialParameterSpell.Parameter) EditorGUILayout.EnumPopup ("Parameter Name", script.parameterName);
		script.value = (float)EditorGUILayout.Slider ("Value", script.value, minLimit, maxLimit);

		PrefabUtility.DisconnectPrefabInstance (target);
	}
}
