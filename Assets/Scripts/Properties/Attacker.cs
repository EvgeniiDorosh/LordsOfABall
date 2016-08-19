using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class Attacker : Creature, IAttacker {
	
	public float GetDamage (CreatureParameters targetParameters) {
		float resultDamage;
		float pureDamage = Random.Range (CurrentParameters.MinimumDamage, CurrentParameters.MaximumDamage);
		float attack = CurrentParameters.Attack;
		float defense = targetParameters.Defense;

		if (attack > defense) {
			resultDamage = pureDamage * (1 + 0.05f * (attack - defense));
		} else {
			resultDamage = pureDamage / (1 + 0.05f * (defense - attack));
		}

		return resultDamage;
	}
}
