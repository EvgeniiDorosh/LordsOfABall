using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestructibleInitializer : MonoBehaviour {

	public string indexName = "empty";

	void Awake () 
	{
		BasicStatsController statsController = GetComponent<BasicStatsController> ();
		PassiveDamageable passiveDamageable = GetComponent<PassiveDamageable> ();

		BaseStat stat = null;
		List<StatBlank> blanks = ConfigsParser.DestructiblesConfig.GetBlanks (indexName);
		if (blanks != null) {
			foreach (var blank in blanks) {
				stat = new BaseClampedStat (blank.type, blank.value, null, 0);
				statsController.Add (stat);
			}
		} 
		else 
		{
			stat = new BaseClampedStat (StatType.Health, passiveDamageable.level, null, 0);
			statsController.Add (stat);
		}

		BaseStat health = statsController.Get (StatType.Health);
		statsController.Add (new BaseClampedStat (StatType.CurrentHealth, health.Value, health));
	}
}
