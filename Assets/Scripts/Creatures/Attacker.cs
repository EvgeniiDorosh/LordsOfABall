using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CreatureParametersController))]
public class Attacker : MonoBehaviour, IAttacker {

	private CreatureParametersController paramsController;

	void Awake() {
		paramsController = GetComponent<CreatureParametersController> ();
	}

	public float GetCurrentValue (string paramName) {
		return paramsController.CurrentParameters.GetValue(paramName);
	}

	public float GetInitialValue (string paramName) {
		return paramsController.InitialParameters.GetValue(paramName);
	}

	public void ChangeParameter (string paramName, float diffValue) {
		paramsController.ChangeParameter (paramName, diffValue);
	}

	public float GetDamage (ICreature creature) {
		float resultDamage;
		float pureDamage = Random.Range (GetCurrentValue("MinimumDamage"), GetCurrentValue("MaximumDamage"));
		float attack = GetCurrentValue("Attack");
		float defense = creature.GetCurrentValue("Defense");

		if (attack > defense) {
			resultDamage = pureDamage * (1 + 0.05f * (attack - defense));
		} else {
			resultDamage = pureDamage / (1 + 0.05f * (defense - attack));
		}

		return resultDamage;
	}

	protected virtual void OnCollisionEnter2D(Collision2D other) {
		IDamageable damageable = other.gameObject.GetComponent<IDamageable> ();
		if (damageable != null) {
			damageable.ApplyDamage (this);
		}
	}
}
