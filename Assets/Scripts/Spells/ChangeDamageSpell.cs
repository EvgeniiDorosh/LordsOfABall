using UnityEngine;
using System.Collections;

public class ChangeDamageSpell : Spell {

	public Parameter parameterName;
	public enum Parameter { MinimumDamage, MaximumDamage}

	private float diffValue;
	public int percentage;

	private ICreature targetCreature;

	override public void Cast() {
		targetCreature = targetObject.GetComponent<ICreature>();
		if (targetCreature != null) {
			float damageGap = (targetCreature.GetInitialValue("MaximumDamage") - targetCreature.GetInitialValue("MinimumDamage"));
			diffValue = ((float)percentage / 100f) * damageGap;
			targetCreature.ChangeParameter(parameterName.ToString(), diffValue);
		}
	}

	override public void Finish() {
		if (targetCreature != null) {
			targetCreature.ChangeParameter(parameterName.ToString(), -diffValue);
		}
	}
}
