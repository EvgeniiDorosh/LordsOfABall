using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour, IAttacker {

	private WeaponParametersController paramsController;

	void Awake() {
		paramsController = GetComponent<WeaponParametersController> ();
	}

	public float GetDamage (ICreature creature) {
		float resultDamage;
		float pureDamage = GetCurrentValue("Damage");
		float attack = GetCurrentValue("Attack");
		float defense = creature.GetCurrentValue ("Defense");
		defense -= defense * GetCurrentValue("DefenseIgnoring") / 100f;

		if (attack > defense) {
			resultDamage = pureDamage * (1 + 0.05f * (attack - defense));
		} else {
			resultDamage = pureDamage / (1 + 0.05f * (defense - attack));
		}

		return resultDamage;
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
}
