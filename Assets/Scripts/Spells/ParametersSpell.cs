using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Collections;

[Serializable]
public class ParametersSpell : Spell {
	
	public Parameter parameterName;
	public enum Parameter { Health, Attack, Defense, MinimumDamage, MaximumDamage, Initiative, Mana, SpellPower, Luck, Morale }

	public ChangeValueType changeValueType;
	public enum ChangeValueType { Absolute, Relative }

	private float diffValue;
	public float exactValue;
	public float minValue;
	public float maxValue;
	public ValueType valueType;
	public enum ValueType { Constant, Random }

	private ICreature targetCreature;

	override public void Cast () {
		targetCreature = targetObject.GetCreature();
		if (targetCreature != null) {
			diffValue = GetDiffValue ();
			targetCreature.ChangeParameter(parameterName.ToString(), diffValue);
		}
	}

	override public void Finish() {
		if (targetCreature != null) {
			targetCreature.ChangeParameter(parameterName.ToString(), -diffValue);
		}
	}

	float GetDiffValue() {
		float result = 0f;
		if (diffValue >= Mathf.Epsilon || diffValue <= -Mathf.Epsilon) {
			result = diffValue;
		} else {
			switch (valueType) {
			case ValueType.Constant:
				result = exactValue;
				break;
			case ValueType.Random:
				result = Random.Range (minValue, maxValue);
				break;
			}

			switch (changeValueType) {
			case ChangeValueType.Relative:
				float currentParameterValue = targetCreature.GetCurrentValue (parameterName.ToString());
				result = currentParameterValue + Mathf.Sign(result) * currentParameterValue * (result / 100);
				break;
			}
		}

		if (result <= Mathf.Epsilon && result >= -Mathf.Epsilon) {
			result = 1.0f;
		}

		return result;
	}
}
