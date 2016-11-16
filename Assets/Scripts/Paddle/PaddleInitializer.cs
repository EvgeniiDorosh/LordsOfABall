using UnityEngine;
using System.Collections;
using System.Reflection;

[RequireComponent(typeof(Paddle))]
[RequireComponent(typeof(PaddleMotionController))]
[RequireComponent(typeof(PaddleParametersController))]
[RequireComponent(typeof(CreatureParametersController))]
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
		int currentLevel = GameController.Instance.CurrentLevel;
		switch (GameController.Instance.CurrentGameMode) {
		case GameMode.Campaign:
			if (currentLevel == 1) {
				InitFromConfig (currentLevel);
			} else {
				InitFromSaveData ();
			}
			break;
		case GameMode.Single: 
			InitFromConfig (currentLevel);
			break;
		case GameMode.Survival: 
			break;
		}
	}

	void InitFromConfig(int currentLevel) {
		PaddleConfig config = ConfigsParser.instance.paddleConfig;
		creatureParamsController.InitialParameters = config.GetInitialParameters();
		creatureParamsController.CurrentParameters = creatureParamsController.InitialParameters.Clone ();
		creatureParamsController.DestinationEvent = PaddleEvent.parameterWasUpdated;

		paddleParamsController.InitialParameters = config.GetPaddleParameters();
		paddleParamsController.CurrentParameters = paddleParamsController.InitialParameters.Clone();
	}

	void InitFromSaveData() {
		PrefsManager prefsManager = PrefsManager.Instance;
		CreatureParameters creatureInitialParams = new CreatureParameters();
		PropertyInfo[] properties = creatureInitialParams.GetType ().GetProperties ();
		foreach (PropertyInfo property in properties) {
			creatureInitialParams.SetValue(property.Name, prefsManager.GetFloat(property.Name));
		}
		creatureParamsController.InitialParameters = creatureInitialParams;
		creatureParamsController.CurrentParameters = creatureParamsController.InitialParameters.Clone ();
		creatureParamsController.DestinationEvent = PaddleEvent.parameterWasUpdated;

		PaddleParameters paddleInitialParams = new PaddleParameters();
		properties = paddleInitialParams.GetType ().GetProperties ();
		foreach (PropertyInfo property in properties) {
			paddleInitialParams.SetValue(property.Name, prefsManager.GetFloat(property.Name));
		}
		paddleParamsController.InitialParameters = paddleInitialParams;
		paddleParamsController.CurrentParameters = paddleParamsController.InitialParameters.Clone();
	}

	void InitUIPanel() {
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
