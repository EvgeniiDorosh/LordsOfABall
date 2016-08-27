using UnityEngine;
using System.Collections;

public class Creature : MonoBehaviour {

	[SerializeField]
	private CreatureParameters initialParameters;
	public CreatureParameters InitialParameters {
		get {
			return initialParameters;
		}
		set {
			initialParameters = value;
		}
	}

	[SerializeField]
	private CreatureParameters currentParameters;
	public CreatureParameters CurrentParameters {
		get {
			return currentParameters;
		}
		set {
			currentParameters = value;
		}
	}

	private ParametersController paramsController;
	public ParametersController ParamsController {
		get { 
			return paramsController;
		}	
	}

	protected void Awake() {
		InitControllers ();
	}

	protected virtual void InitControllers() {
		paramsController = new ParametersController (this);
	}
}
