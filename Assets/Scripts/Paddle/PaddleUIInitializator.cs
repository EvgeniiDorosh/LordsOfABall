using UnityEngine;
using System.Collections;
using System.Reflection;

public class PaddleUIInitializator : MonoBehaviour {

	private Paddle paddle;
	private ParametersController paramsController;
	private CreatureParameters creatureParameters;

	void Awake () {
		paddle = GetComponent<Paddle> ();
		paramsController = paddle.ParamsController;
		creatureParameters = paddle.CurrentParameters;
	}

	public void InitUIPanel() {
		PropertyInfo[] properties = creatureParameters.GetType ().GetProperties ();
		foreach (PropertyInfo property in properties) {
			paramsController.ChangeParameter(property.Name, 0.0f);
			paramsController.ChangeParameter("Initial" + property.Name, 0.0f);
		}
	}
}
