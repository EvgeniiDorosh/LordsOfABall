using UnityEngine;
using System;
using System.Collections;

public class CreaturesConfig<T, U> where T : CreatureParametersConfig
								   where U : BaseCreatureParameters, new() {

	public T[] parameters;

	protected virtual U ConvertConfigToParameters(T config) {
		U result = new U ();
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
