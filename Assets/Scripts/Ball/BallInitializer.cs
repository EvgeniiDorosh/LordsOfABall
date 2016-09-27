using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CreatureParametersController))]
[RequireComponent(typeof(BallMotionController))]
public class BallInitializer : MonoBehaviour {
	
	private CreatureParametersController paramsController;
	private BallMotionController ballMotionController;

	void Awake () {
		GameObject paddle = GameObject.FindGameObjectWithTag ("Player");
		CreatureParametersController paddleParamsController = paddle.GetComponent<CreatureParametersController> ();

		paramsController = GetComponent<CreatureParametersController> ();
		paramsController.InitialParameters = paddleParamsController.InitialParameters.Clone();
		paramsController.CurrentParameters = paddleParamsController.CurrentParameters.Clone ();
		paramsController.DestinationEvent = BallEvent.parameterWasUpdated;

		PaddleParametersController paddleController = paddle.GetComponent<PaddleParametersController>();
		ballMotionController = GetComponent<BallMotionController> ();
		ballMotionController.InitialSpeed = ballMotionController.CurrentSpeed = paddleController.CurrentParameters.Speed;
	}
	
	void OnEnable() {
		Messenger<StatChange>.AddListener (PaddleEvent.parameterWasUpdated, OnPaddleParameterUpdate);
	}

	void OnDisable(){
		Messenger<StatChange>.RemoveListener (PaddleEvent.parameterWasUpdated, OnPaddleParameterUpdate);
	}

	void OnPaddleParameterUpdate(StatChange statChange) {
		paramsController.ChangeParameter (statChange.name, statChange.diff);
	}
}
