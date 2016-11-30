using UnityEngine;
using System.Collections;

public class DestructibleParametersController : ParametersController {

	protected DestructibleParameters initialParameters;
	public DestructibleParameters InitialParameters {
		get { return initialParameters; }
		set { initialParameters = value; }
	}

	protected DestructibleParameters currentParameters;
	public DestructibleParameters CurrentParameters {
		get { return currentParameters; }
		set { currentParameters = value; }
	}

	private string destinationEvent = "";
	public string DestinationEvent {
		get { return destinationEvent; }
		set { destinationEvent = value; }
	}

	/**
	 * Change methods
	 */

	private void ChangeHealth(float diff) {
		currentParameters.Health += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Health", initialParameters.Health, currentParameters.Health, diff));
	}

	private void ChangeInitialHealth(float diff) {
		initialParameters.Health += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialHealth", initialParameters.Health, initialParameters.Health, diff));
	}

	private void ChangeDefense(float diff) {
		currentParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("Defense", initialParameters.Defense, currentParameters.Defense, diff));
	}

	private void ChangeInitialDefense(float diff) {
		initialParameters.Defense += diff;
		Messenger<StatChange>.Invoke (destinationEvent, new StatChange("InitialDefense", initialParameters.Defense, initialParameters.Defense, diff));
		ChangeDefense (diff);
	}
}
