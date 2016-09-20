using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ParametersSpell))]
public class ParameterSpellEditor : Editor {

	float minLimit = -100f;
	float maxLimit = 100f;

	public override void OnInspectorGUI() {

		var script = target as ParametersSpell;

		script.itemName = EditorGUILayout.TextField ("Spell Name", script.itemName);

		script.parameterName = (ParametersSpell.Parameter) EditorGUILayout.EnumPopup ("Parameter Name", script.parameterName);
		script.changeValueType = (ParametersSpell.ChangeValueType) EditorGUILayout.EnumPopup ("Change Value Type", script.changeValueType);
		string valueString = script.changeValueType == ParametersSpell.ChangeValueType.Absolute ? "Value" : "Value, %";

		script.valueType = (ParametersSpell.ValueType)EditorGUILayout.EnumPopup ("Value Type", script.valueType);
		if (script.valueType == ParametersSpell.ValueType.Constant) {			
			script.exactValue = (float)EditorGUILayout.Slider (valueString, script.exactValue, minLimit, maxLimit);
		} else {
			EditorGUILayout.LabelField ("Range");
			EditorGUI.indentLevel++;
			script.minValue = (float)EditorGUILayout.Slider ("Min " + valueString, Mathf.Clamp(script.minValue, minLimit, script.maxValue), minLimit, maxLimit);
			script.maxValue = (float)EditorGUILayout.Slider ("Max " + valueString, Mathf.Clamp(script.maxValue, script.minValue, maxLimit), minLimit, maxLimit);
			EditorGUI.indentLevel--;
		}

		script.temporalType = (ParametersSpell.TemporalType) EditorGUILayout.EnumPopup ("Temporal Type", script.temporalType);
		if (script.temporalType == ParametersSpell.TemporalType.Temporary) {
			EditorGUILayout.LabelField ("Duration");
			EditorGUI.indentLevel++;
			script.duration.minutes = (int)EditorGUILayout.Slider ("Minutes", script.duration.minutes, 0, 4);
			script.duration.seconds = (int)EditorGUILayout.Slider ("Seconds", script.duration.seconds, 0, 59);
			EditorGUI.indentLevel--;
		}

		PrefabUtility.DisconnectPrefabInstance (target);
	}
}
