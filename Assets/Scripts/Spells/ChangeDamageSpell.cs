using UnityEngine;
using System.Collections;

public class ChangeDamageSpell : Spell {

	public Parameter parameterName;
	public enum Parameter { MinimumDamage, MaximumDamage}

	private float diffValue;
	public int percentage;

	private Creature targetCreature;

	override public void Cast() {
		targetCreature = GetCreature(targetObject);
		if (targetCreature != null) {
			float damageGap = (targetCreature.InitialParameters.MaximumDamage - targetCreature.InitialParameters.MinimumDamage);
			diffValue = ((float)percentage / 100f) * damageGap;
			targetCreature.ParamsController.ChangeParameter(parameterName.ToString(), diffValue);
		}
	}

	override public void Finish() {
		if (targetCreature != null) {
			targetCreature.ParamsController.ChangeParameter(parameterName.ToString(), -diffValue);
		}
	}

	Creature GetCreature(GameObject targetObject) {
		Creature result = targetObject.GetComponent<Creature> ();
		if (result == null) {
			result = targetObject.GetComponentInParent<Creature> ();
		}
		if (result == null) {
			result = targetObject.GetComponentInChildren<Creature> ();
		}

		return result;
	}
}
