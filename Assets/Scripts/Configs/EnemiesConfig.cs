using UnityEngine;
using System;
using System.Collections;

public class EnemiesConfig : CreaturesConfig<EnemyParameteresConfig, BaseCreatureParameters> {
	
	public BaseCreatureParameters GetParametersByName(string name) {
		foreach (EnemyParameteresConfig config in parameters) {
			if (config.name == name) {
				return ConvertConfigToParameters(config);
			}
		}
		return new BaseCreatureParameters ();
	}
}
