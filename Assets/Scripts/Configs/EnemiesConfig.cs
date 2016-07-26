using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemiesConfig {
	
	public EnemyParameteresConfig[] parameters;

	public BaseCreatureParameters GetParametersByName(string name) {
		foreach (EnemyParameteresConfig config in parameters) {
			if (config.name == name) {
				return ConvertConfigToParameters(config);
			}
		}
		return new BaseCreatureParameters ();
	}

	private BaseCreatureParameters ConvertConfigToParameters(EnemyParameteresConfig config) {
		BaseCreatureParameters result = new BaseCreatureParameters ();
		result.Health = config.health;
		result.Attack = config.attack;
		result.Defense = config.defense;
		result.MinimumDamage = config.minimumDamage;
		result.MaximumDamage = config.maximumDamage;
		result.Mana = config.mana;
		result.SpellPower = config.spellPower;

		return result;
	}
}
