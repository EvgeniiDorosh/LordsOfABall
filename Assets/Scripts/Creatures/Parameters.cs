using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public abstract class Parameters {

	private Dictionary<string, PropertyInfo> properties = new Dictionary<string, PropertyInfo> ();

	public float GetValue(string paramName) {
		float result = 0f;

		PropertyInfo property = GetType ().GetProperty (paramName);

		//if(properties.TryGetValue)

		try {
			result = (float) property.GetValue (this, null);
		} catch(NullReferenceException) {
			Debug.LogError ("GetValue: Property " + paramName + " doesnt exist!");	
		}

		return result;
	}

	public void SetValue(string paramName, float value) {
		PropertyInfo property = GetType ().GetProperty (paramName);
		try {
			property.SetValue (this, value, null);
		} catch(NullReferenceException) {
			Debug.LogError ("SetValue: Property " + paramName + " doesnt exist!");	
		}
	}

	public bool HasValue(string paramName) {
		PropertyInfo property = GetType ().GetProperty (paramName);
		return property != null;
	}
}
