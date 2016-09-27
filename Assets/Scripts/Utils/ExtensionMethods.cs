using UnityEngine;
using System.Collections;

public static class ExtensionMethods {

	public static ICreature GetCreature(this GameObject targetObject) {
		ICreature result = targetObject.GetComponent<ICreature> ();
		if (result == null) {
			result = targetObject.GetComponentInParent<ICreature> ();
		}
		if (result == null) {
			result = targetObject.GetComponentInChildren<ICreature> ();
		}

		return result;
	}
}
