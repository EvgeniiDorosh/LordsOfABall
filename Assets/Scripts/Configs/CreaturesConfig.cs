using UnityEngine;
using System;
using System.Collections;

public class CreaturesConfig<T> where T : CreatureParametersConfig {

	public T[] parameters;

	protected virtual CreatureParameters ConvertConfigToParameters(T config) {
		CreatureParameters result = new CreatureParameters ();
		result.Health = config.health;
		result.Attack = config.attack;
		result.Defense = config.defense;
		result.MinimumDamage = config.minimumDamage;
		result.MaximumDamage = config.maximumDamage;
		result.Initiative = config.initiative;
		result.Mana = config.mana;
		result.SpellPower = config.spellPower;
		result.Luck = config.luck;
		result.Morale = config.morale;

		return result;
	}
}
