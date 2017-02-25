using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Stat : BaseClampedStat
{
	protected List<StatModifier> modifiers = new List<StatModifier> ();

	protected float modifiersValue = 0;

	#region Constructors

	public Stat() : base() { }

	public Stat(StatType type, float value) : base(type, value) { }

	public Stat(StatType type, float value, BaseStat maxStat, BaseStat minStat = null) : base(type, value, maxStat, minStat) { }

	public Stat(StatType type, float value, float maxValue, float minValue = 0) : base(type, value, maxValue, minValue) { }

	public Stat(StatType type, float value, BaseStat maxStat, float minValue = 0) : base(type, value, maxStat, minValue) { }

	public Stat(StatType type, float value, float maxValue, BaseStat minStat) : base(type, value, maxValue, minStat) { }

	#endregion

	public float RawModifiersValue
	{
		get { return modifiersValue;}
	}

	public float ModifiersValue
	{
		get { return Value - RawValue;}
	}

	public override float Value 
	{
		get { return Mathf.Clamp(RawValue + modifiersValue, MinValue, MaxValue); }
	}

	override protected float MinValue
	{
		get { return minStat != null ? minStat.Value : minValue; }
	}

	override protected float MaxValue
	{
		get { return maxStat != null ? maxStat.Value : maxValue; }
	}

	public virtual void AddModifier(StatModifier value)
	{
		modifiers.Add (value);
		modifiersValue = UpdateModifiedValue ();
		OnValueChanged ();
	}

	public virtual void RemoveModifier(StatModifier value)
	{
		modifiers.Remove (value);
		modifiersValue = UpdateModifiedValue ();
		OnValueChanged ();
	}

	public virtual void RemoveModifier(StatModifierType type)
	{
		modifiers.RemoveAll (item => item.EffectType == type);
		modifiersValue = UpdateModifiedValue ();
		OnValueChanged ();
	}

	public virtual void ClearModifiers()
	{
		modifiers.Clear ();
		modifiersValue = 0;
		OnValueChanged ();
	}

	protected virtual float UpdateModifiedValue()
	{
		float result = 0;
		float baseDiff = 0, baseRelativeDiff = 0;

		foreach (StatModifier modifier in modifiers) 
		{
			switch (modifier.EffectType) 
			{
			case StatModifierType.BaseValue:
				baseDiff += modifier.Value;
				break;
			case StatModifierType.BaseValuePercent:
				baseRelativeDiff += modifier.Value;
				break;
			default:
				break;
			}
		}

		result = (base.Value * baseRelativeDiff) + baseDiff;

		return result;
	}
}


