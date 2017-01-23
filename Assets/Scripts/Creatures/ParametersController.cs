using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

public abstract class ParametersController : MonoBehaviour {

	private string key = "Change";

	protected readonly Dictionary<string, Action<float>> changeMethods = new Dictionary<string, Action<float>>();

	public void ChangeParameter(string paramName, float diffValue) {

		if(changeMethods.ContainsKey(paramName)) {
			changeMethods[paramName].Invoke(diffValue);
		} else {
			MethodInfo method = GetType ().GetMethod (key + paramName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (method != null) {
				Action<float> action = (Action<float>)Delegate.CreateDelegate(typeof(Action<float>), this, method);
				changeMethods.Add (paramName, action);
				action.Invoke (diffValue);
			}
		}
	}
}
