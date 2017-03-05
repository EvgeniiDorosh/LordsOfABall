using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour, IAttacker 
{
	private StatsController statsController;

	void Awake() 
	{
		statsController = GetComponent<StatsController> ();
	}

	public float GetStatValue(StatType type)
	{
		return statsController.Get<BaseStat> (type).Value;
	}

	public void ChangeStatValue (StatType type, float diffValue) 
	{
		statsController.ChangeValue<BaseStat>(type, diffValue);
	}

	public float GetDamage (ICreature creature) {
		float resultDamage;
		float pureDamage = GetStatValue(StatType.Damage);
		float attack = GetStatValue(StatType.Attack);
		float defense = creature.GetStatValue (StatType.Defense);
		defense -= defense * GetStatValue(StatType.DefenseIgnoring) / 100f;

		if (attack > defense) {
			resultDamage = pureDamage * (1 + 0.05f * (attack - defense));
		} else {
			resultDamage = pureDamage / (1 + 0.05f * (defense - attack));
		}

		return resultDamage;
	}
}
