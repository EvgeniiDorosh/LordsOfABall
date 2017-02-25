using System;
using UnityEngine;

[Serializable]
public class BaseClampedStat : BaseStat
{
	protected float minValue = 0;
	protected float maxValue = Mathf.Infinity;

	protected BaseStat maxStat;
	protected BaseStat minStat;

	#region Constructors

	public BaseClampedStat () : base() { }

	public BaseClampedStat(StatType type, float value) : base(type, value) { }

	public BaseClampedStat(StatType type, float value, BaseStat maxStat, BaseStat minStat = null) : base(type, value) 
	{ 
		SetMax(maxStat);
		SetMin(minStat);
	}

	public BaseClampedStat(StatType type, float value, float maxValue, float minValue = 0) : base(type, value) 
	{
		SetMax(maxValue);
		SetMin(minValue);
	}

	public BaseClampedStat(StatType type, float value, BaseStat maxStat, float minValue) : base(type, value) 
	{ 
		SetMax(maxStat);
		SetMin(minValue);
	}

	public BaseClampedStat(StatType type, float value, float maxValue, BaseStat minStat) : base(type, value) 
	{ 
		SetMax(maxValue);
		SetMin(minStat);
	}

	#endregion

	public override float Value
	{
		set { base.Value = Mathf.Clamp (value, MinValue, MaxValue);	}
	}

	protected virtual float MinValue
	{
		get { return minStat != null ? minStat.RawValue : minValue; }
	}

	protected virtual float MaxValue
	{
		get { return maxStat != null ? maxStat.RawValue : maxValue; }
	}

	public void SetMin(BaseStat stat)
	{
		minStat = stat;
	}

	public void SetMin(float value)
	{
		minValue = value;
	}

	public void SetMax(BaseStat stat)
	{
		maxStat = stat;
	}

	public void SetMax(float value)
	{
		maxValue = value;
	}
}

