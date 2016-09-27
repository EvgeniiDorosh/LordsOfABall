using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CreatureParametersController))]
public class EnemyInitilizer : MonoBehaviour {

	private CreatureParametersController paramsController;
	public string creatureName;

	void Awake () {
		paramsController = GetComponent<CreatureParametersController> ();
		paramsController.InitialParameters = ConfigsParser.instance.enemiesConfig.GetParametersByName (creatureName);
		paramsController.CurrentParameters = paramsController.InitialParameters.Clone();
	}
}
