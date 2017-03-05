using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInitilizer : MonoBehaviour {

	private BasicStatsController statsController;
	public string creatureName;

	void Awake () 
	{
		statsController = GetComponent<BasicStatsController> ();

		Stat stat = null;
		List<StatBlank> blanks = ConfigsParser.EnemiesConfig.GetBlanks (creatureName);
		foreach (var blank in blanks) 
		{
			stat = new Stat (blank.type, blank.value, null, 0);
			statsController.Add(stat);
		}

		Stat health = statsController.Get<Stat> (StatType.Health);
		Stat mana = statsController.Get<Stat> (StatType.Mana);

		statsController.Add (new BaseClampedStat (StatType.CurrentHealth, health.Value, health));
		statsController.Add (new BaseClampedStat (StatType.CurrentMana, mana.Value, null, 0));

		Stat maxDamage = statsController.Get<Stat> (StatType.MaximumDamage);
		Stat minDamage = statsController.Get<Stat> (StatType.MinimumDamage);
		minDamage.SetMax (maxDamage);
	}
}
