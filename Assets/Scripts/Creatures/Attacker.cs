using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

// TODO: Make it abstract;
public class Attacker : MonoBehaviour, IAttacker {

	private BasicStatsController statsController;

	void Awake() 
	{
		statsController = GetComponent<BasicStatsController> ();
	}

	public float GetStatValue(StatType type)
	{
		return statsController.Get<BaseStat> (type).Value;
	}

	public void ChangeStatValue (StatType type, float diffValue) 
	{
		statsController.ChangeValue<BaseStat>(type, diffValue);
	}

	public float GetDamage (ICreature creature) 
	{
		float resultDamage;
		float pureDamage = CalculatePureDamage();
		float attack = GetStatValue(StatType.Attack);
		float defense = creature.GetStatValue(StatType.Defense);

		if (attack > defense) 
		{
			resultDamage = pureDamage * (1 + 0.05f * (attack - defense));
		} 
		else 
		{
			resultDamage = pureDamage / (1 + 0.05f * (defense - attack));
		}

		return resultDamage;
	}

	protected virtual void OnCollisionEnter2D(Collision2D other) 
	{
		IDamageable damageable = other.gameObject.GetComponent<IDamageable> ();
		if (damageable != null) 
		{
			damageable.ApplyDamage (this);
		}
	}

	float CalculatePureDamage()
	{
		float min = GetStatValue (StatType.MinimumDamage);
		float max = GetStatValue (StatType.MaximumDamage);
		return Random.Range(min, max);
	}
}
