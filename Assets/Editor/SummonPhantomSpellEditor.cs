using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SummonPhantomSpell))]
public class SummonPhantomSpellEditor : Editor {
	
	public override void OnInspectorGUI() {

		var script = target as SummonPhantomSpell;

		script.itemName = EditorGUILayout.TextField ("Spell Name", script.itemName);
		script.temporalType = Spell.TemporalType.Single;
		script.ballsAmount = (int)EditorGUILayout.Slider ("Balls Amount", script.ballsAmount, 1, 8);
	}
}
