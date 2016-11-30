using UnityEngine;
using System.Collections;

public class PaddleParametersController : ParametersController {

	protected PaddleParameters initialParameters;
	public PaddleParameters InitialParameters {
		get { return initialParameters; }
		set { initialParameters = value; }
	}

	protected PaddleParameters currentParameters;
	public PaddleParameters CurrentParameters {
		get { return currentParameters; }
		set { currentParameters = value; }
	}

	private void ChangeWidth(float diff) {
		float oldValue = currentParameters.Width;
		currentParameters.Width = Mathf.Clamp(oldValue + diff, 0.5f, 2.0f);
		float trueDiff = currentParameters.Width - oldValue;
		Messenger<StatChange>.Invoke (PaddleEvent.widthWasUpdated, new StatChange("Width", initialParameters.Width, currentParameters.Width, trueDiff));
	}

	private void ChangeInitialWidth(float diff) {
		float oldValue = initialParameters.Width;
		initialParameters.Width = Mathf.Clamp(oldValue + diff, 0.5f, 2.0f);
		float trueDiff = initialParameters.Width - oldValue;
		Messenger<StatChange>.Invoke (PaddleEvent.widthWasUpdated, new StatChange("InitialWidth", initialParameters.Width, initialParameters.Width, trueDiff));
		ChangeWidth (diff);
	}

	private void ChangeSpeed(float diff) {
		currentParameters.Speed += diff;
		Messenger<StatChange>.Invoke (PaddleEvent.speedWasUpdated, new StatChange("Speed", initialParameters.Speed, currentParameters.Speed, diff));
	}

	private void ChangeInitialSpeed(float diff) {
		initialParameters.Speed += diff;
		Messenger<StatChange>.Invoke (PaddleEvent.speedWasUpdated, new StatChange("InitialSpeed", initialParameters.Speed, initialParameters.Speed, diff));
		ChangeSpeed (diff);
	}
}
