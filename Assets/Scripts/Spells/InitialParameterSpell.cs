using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class InitialParameterSpell : Spell {

	public Parameter parameterName;
	public enum Parameter { Health, Attack, Defense, MinimumDamage, MaximumDamage, Initiative, Mana, SpellPower, Luck, Morale }

	public float value;

	void Start() {
		temporalType = TemporalType.Single;
	}

	override public void Cast () {
		ICreature targetCreature = targetObject.GetComponent<ICreature>();
		if (targetCreature != null) {
			targetCreature.ChangeParameter("Initial" + parameterName.ToString(), value);
		}
	}

	override public void Finish() { }
}
