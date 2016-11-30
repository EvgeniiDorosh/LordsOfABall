using UnityEngine;
using System.Collections;

public class AbyssParametersController : ParametersController {

	protected AbyssParameters parameters;
	public AbyssParameters Parameters {
		get { return parameters; }
		set { parameters = value; }
	}

	/**
	 * Change methods
	 */

	private void ChangeAttack(float diff) {
		parameters.Attack += diff;
	}

	private void ChangeDamage(float diff) {
		parameters.Damage += diff;
	}
}
