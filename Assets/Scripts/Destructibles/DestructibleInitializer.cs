using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(DestructibleParametersController))]
[RequireComponent(typeof(PassiveDamageable))]
[RequireComponent(typeof(Destructible))]
public class DestructibleInitializer : MonoBehaviour {

	public string indexName = "empty";

	void Awake () {
		DestructibleParametersController paramsController = GetComponent<DestructibleParametersController> ();
		PassiveDamageable passiveDamageable = GetComponent<PassiveDamageable> ();
		paramsController.InitialParameters = ConfigsParser.instance.destructiblesConfig.GetParametersByName (indexName);
		if (passiveDamageable.isStable) {
			paramsController.InitialParameters.SetValue ("Health", passiveDamageable.level);
		}
		paramsController.CurrentParameters = paramsController.InitialParameters.Clone();
		paramsController.DestinationEvent = gameObject.GetInstanceID ().ToString ();
	}
}
