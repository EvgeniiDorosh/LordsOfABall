using UnityEngine;
using System.Collections;

public static class ExtensionMethods {

	public static CreatureVO GetCreatureVO(this GameObject target) {
		CreatureVO result = new CreatureVO ();
		result.name = target.name;
		result.tag = target.tag;
		result.instanceID = target.GetInstanceID();
		result.position = target.transform.position;

		return result;
	}
}
