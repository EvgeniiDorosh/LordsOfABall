using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	public CreatureParameters InitialParameters {
		get { return paramsController.InitialParameters;}
		set { paramsController.InitialParameters = value;}
	}

	public CreatureParameters CurrentParameters {
		get { return paramsController.CurrentParameters;}
		set { paramsController.CurrentParameters = value;}
	}

	private ParametersController paramsController;
	public ParametersController ParamsController {
		get { return paramsController;}	
	}

	public void ChangeParameter(string paramName, float diffValue) {
		paramsController.ChangeParameter (paramName, diffValue);
	}

	protected void Awake() {
		paramsController = new ParametersController ();
	}
}
