﻿using UnityEngine;
using System;
using System.Reflection;

public abstract class Parameters {

	public float GetValue(string paramName) {
		float result = 0f;
		PropertyInfo property = GetType ().GetProperty (paramName);
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
}
