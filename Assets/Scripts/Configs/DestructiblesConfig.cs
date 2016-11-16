using UnityEngine;
using System.Collections;

public class DestructiblesConfig {

	public DestructibleParametersConfig[] parameters;

	public DestructibleParameters GetParametersByName(string name) {
		foreach (DestructibleParametersConfig config in parameters) {
			if (config.name == name) {
				return ConvertConfigToParameters(config);
			}
		}
		return new DestructibleParameters ();
	}

	private DestructibleParameters ConvertConfigToParameters(DestructibleParametersConfig config) {
		DestructibleParameters result = new DestructibleParameters ();
		result.Health = config.health;
		result.Defense = config.defense;

		return result;
	}
}

