using UnityEngine;
using System;
using System.Collections;

public class EnemiesConfig : CreaturesConfig<EnemyParameteresConfig> {
	
	public CreatureParameters GetParametersByName(string name) {
		foreach (EnemyParameteresConfig config in parameters) {
			if (config.name == name) {
				return ConvertConfigToParameters(config);
			}
		}
		return new CreatureParameters ();
	}
}
