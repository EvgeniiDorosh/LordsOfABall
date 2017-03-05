using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponInitializer : MonoBehaviour {

	private BasicStatsController statsController;
	public string weaponName;

	void Awake () 
	{
		statsController = GetComponent<BasicStatsController> ();
		Stat stat = null;
		List<StatBlank> blanks = ConfigsParser.WeaponsConfig.GetBlanks (weaponName);
		foreach (var blank in blanks) 
		{
			stat = new Stat (blank.type, blank.value, null, 0);
			statsController.Add(stat);
		}
	}
}
