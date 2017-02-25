using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStatsController : MonoBehaviour 
{
	[SerializeField]
	protected List<Stat> stats;

	protected BaseStatsSet statsSet = new BaseStatsSet();

	public virtual void Add(BaseStat stat)
	{
		statsSet.Add (stat.Type, stat);
	}

	public BaseStat Get(StatType type)
	{
		return statsSet.Get(type);
	}

	public T Get<T>(StatType type) where T : BaseStat
	{
		return statsSet.Get<T>(type) as T;
	}

	public virtual T ChangeValue<T>(StatType type, float diff) where T : BaseStat
	{
		return statsSet.ChangeValue<T> (type, diff);
	}
}
