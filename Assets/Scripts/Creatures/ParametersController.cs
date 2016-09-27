using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public abstract class ParametersController : MonoBehaviour {

	protected readonly Dictionary<string, Action<float>> changeMethods = new Dictionary<string, Action<float>>();

	public void ChangeParameter(string paramName, float diffValue) {

		string methodName = "Change" + paramName;
		if(changeMethods.ContainsKey(methodName)) {
			changeMethods[methodName].Invoke(diffValue);
		} else {
			MethodInfo method = GetType ().GetMethod (methodName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (method != null) {
				Action<float> action = (Action<float>)Delegate.CreateDelegate(typeof(Action<float>), this, method);
				changeMethods.Add (methodName, action);
				action.Invoke (diffValue);
			}
		}
	}
}
