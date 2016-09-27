using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CreatureParametersController))]
public class ParametricDestructible : MonoBehaviour {

	public string indexName;

	private CreatureParametersController paramsController;

	void Awake () {
		paramsController = GetComponent<CreatureParametersController> ();
		paramsController.InitialParameters = ConfigsParser.instance.destructiblesConfig.GetParametersByName (indexName);
		paramsController.CurrentParameters = paramsController.InitialParameters.Clone();
	}
}
