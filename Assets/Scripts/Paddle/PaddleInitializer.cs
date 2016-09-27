using UnityEngine;
using System.Collections;
using System.Reflection;

public class PaddleInitializer : MonoBehaviour {
	
	private CreatureParametersController creatureParamsController;
	private PaddleParametersController paddleParamsController;

	void Awake () {
		creatureParamsController = GetComponent<CreatureParametersController> ();
		paddleParamsController = GetComponent<PaddleParametersController> ();
		InitParameters ();
		InitUIPanel ();
	}

	void InitParameters() {
		PaddleConfig config = ConfigsParser.instance.paddleConfig;
		creatureParamsController.InitialParameters = config.GetInitialParameters();
		creatureParamsController.CurrentParameters = creatureParamsController.InitialParameters.Clone ();
		creatureParamsController.DestinationEvent = PaddleEvent.parameterWasUpdated;

		paddleParamsController.InitialParameters = config.GetPaddleParameters();
		paddleParamsController.CurrentParameters = paddleParamsController.InitialParameters.Clone();
	}


	private void InitUIPanel() {
		PropertyInfo[] properties = creatureParamsController.CurrentParameters.GetType ().GetProperties ();
		foreach (PropertyInfo property in properties) {
			creatureParamsController.ChangeParameter(property.Name, 0.0f);
			creatureParamsController.ChangeParameter("Initial" + property.Name, 0.0f);
		}

		properties = paddleParamsController.CurrentParameters.GetType ().GetProperties ();
		foreach (PropertyInfo property in properties) {
			paddleParamsController.ChangeParameter(property.Name, 0.0f);
			paddleParamsController.ChangeParameter("Initial" + property.Name, 0.0f);
		}
	}
}
