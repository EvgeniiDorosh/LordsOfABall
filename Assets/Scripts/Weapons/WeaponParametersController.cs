using UnityEngine;
using System.Collections;

public class WeaponParametersController : ParametersController {

	protected WeaponParameters initialParameters;
	public WeaponParameters InitialParameters {
		get { return initialParameters; }
		set { initialParameters = value; }
	}

	protected WeaponParameters currentParameters;
	public WeaponParameters CurrentParameters {
		get { return currentParameters; }
		set { currentParameters = value; }
	}

	/**
	 * Change methods
	 */

	private void ChangeAttack(float diff) {
		currentParameters.Attack += diff;
	}

	private void ChangeInitialAttack(float diff) {
		initialParameters.Attack += diff;
		ChangeAttack (diff);
	}

	private void ChangeDamage(float diff) {
		currentParameters.Damage += diff;
	}

	private void ChangeInitialDamage(float diff) {
		initialParameters.Damage += diff;
		ChangeDamage (diff);
	}

	private void ChangeFirerate(float diff) {
		currentParameters.Firerate += diff;
	}

	private void ChangeInitialFirerate(float diff) {
		initialParameters.Firerate += diff;
		ChangeFirerate (diff);
	}

	private void ChangeSpeed(float diff) {
		currentParameters.Speed += diff;
	}

	private void ChangeInitialSpeed(float diff) {
		initialParameters.Speed += diff;
		ChangeSpeed (diff);
	}

	private void ChangeDefenseIgnoring(float diff) {
		currentParameters.DefenseIgnoring += diff;
	}

	private void ChangeInitialDefenseIgnoring(float diff) {
		initialParameters.DefenseIgnoring += diff;
		ChangeDefenseIgnoring (diff);
	}
}
