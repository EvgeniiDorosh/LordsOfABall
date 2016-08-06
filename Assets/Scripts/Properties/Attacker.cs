using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class Attacker : MonoBehaviour, IAttacker {

	[SerializeField]
	private BaseCreatureParameters initialParameters;
	protected virtual BaseCreatureParameters InitialParameters {
		get {
			return initialParameters;
		}
		set {
			initialParameters = value;
		}
	}

	[SerializeField]
	private BaseCreatureParameters currentParameters;
	protected virtual BaseCreatureParameters CurrentParameters {
		get {
			return currentParameters;
		}
		set {
			currentParameters = value;
		}
	}

	public float GetDamage (BaseCreatureParameters targetParameters) {
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
