using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleDestructible : Destructible {

	public string indexName;

	new void Awake () {
		base.Awake ();
		ParamsController.InitialParameters = ConfigsParser.instance.destructiblesConfig.GetParametersByName (indexName);
		ParamsController.CurrentParameters = InitialParameters.Clone();
		destructibles.Add (this.gameObject);
	}

	void OnDestroy() {
		destructibles.Remove (this.gameObject);
	}
}
