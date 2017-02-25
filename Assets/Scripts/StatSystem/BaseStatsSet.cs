using System;
using System.Collections.Generic;

public class BaseStatsSet
{
	Dictionary<StatType, BaseStat> stats = new Dictionary<StatType, BaseStat>();

	public virtual void Add(BaseStat stat)
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

	public T Get<T>(StatType type) where T : BaseStat
	{
		return Get(type) as T;
	}

	public bool Contains(StatType type)
	{
		return stats.ContainsKey (type);
	}

	public virtual T ChangeValue<T>(StatType type, float diff) where T : BaseStat
	{
		T stat = Get<T> (type);
		if(stat != null)
			stat.Value = stat.Value + diff;
		return stat;
	}
}


