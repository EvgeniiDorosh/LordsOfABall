using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ChangeDamageSpell))]
public class ChangeDamageSpellEditor : Editor {

	int minLimit = -100;
	int maxLimit = 100;

	public override void OnInspectorGUI() {

		var script = target as ChangeDamageSpell;

		script.itemName = EditorGUILayout.TextField ("Spell Name", script.itemName);

		script.parameterName = (ChangeDamageSpell.Parameter) EditorGUILayout.EnumPopup ("Parameter Name", script.parameterName);

		if (script.parameterName == ChangeDamageSpell.Parameter.MinimumDamage) {			
			script.percentage = (int)EditorGUILayout.Slider (new GUIContent("Value, %", "Regarding to maximum damage"), script.percentage, 0, maxLimit);
		} else {
			script.percentage = (int)EditorGUILayout.Slider (new GUIContent("Value, %", "Regarding to minimum damage"), script.percentage, minLimit, 0);
		}

		script.temporalType = (ChangeDamageSpell.TemporalType) EditorGUILayout.EnumPopup ("Temporal Type", script.temporalType);
		if (script.temporalType == ChangeDamageSpell.TemporalType.Temporary) {
			EditorGUILayout.LabelField ("Duration");
			EditorGUI.indentLevel++;
			script.duration.minutes = (int)EditorGUILayout.Slider ("Minutes", script.duration.minutes, 0, 4);
			script.duration.seconds = (int)EditorGUILayout.Slider ("Seconds", script.duration.seconds, 0, 59);
			EditorGUI.indentLevel--;
		}

		PrefabUtility.DisconnectPrefabInstance (target);
	}
}
