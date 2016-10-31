using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class InitialParameterSpell : Spell {

	public static List<InitialParameterSpell> createdInitialSpells = new List<InitialParameterSpell> ();

	public Parameter parameterName;
	public enum Parameter { Health, Attack, Defense, MinimumDamage, MaximumDamage, Initiative, Mana, SpellPower, Luck, Morale }

	public float value;

	void Start() {
		temporalType = TemporalType.Single;
	}

	void OnEnable() {
		createdInitialSpells.Add (this);
	}

	void OnDisable() {
		createdInitialSpells.Remove (this);
	}

	override public void Cast () {
		ICreature targetCreature = targetObject.GetComponent<ICreature>();
		if (targetCreature != null) {
			targetCreature.ChangeParameter("Initial" + parameterName.ToString(), value);
		}
	}

	override public void Finish() { }
}
