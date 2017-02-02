using System;
using System.Collections.Generic;

public class StatsSet
{
	Dictionary<StatType, BaseStat> stats = new Dictionary<StatType, BaseStat>();

	public void Add(BaseStat stat)
	{
		stats.Add (stat.Type, stat);
	}

	public BaseStat Get(StatType type)
	{
		if (Contains (type)) 
		{
			return stats[type];
		}
		return null;
	}

	public bool Contains(StatType type)
	{
		return stats.ContainsKey (type);
	}
}


