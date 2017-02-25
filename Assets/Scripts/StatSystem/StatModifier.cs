using System;
using UnityEngine;

[Serializable]
public class StatModifier
{
	[SerializeField]
	StatModifierType effectType;
	[SerializeField]
	StatType targetType;
	[SerializeField]
	float value;
	[SerializeField]
	float duration;

	public StatModifier (StatModifierType effectType, StatType targetType, float value, float duration = Mathf.Infinity)
	{
		this.effectType = effectType;
		this.targetType = targetType;
		this.value = value;
		this.duration = duration;
	}

	public StatModifierType EffectType
	{
		get { return effectType;}
	}

	public StatType TargetType
	{
		get { return targetType;}
	}

	public float Value
	{
		get { return value;}	
	}

	public float Duration
	{
		get { return duration;}
	}

	public float ChangeDuration(float diff)
	{
		duration += diff;
		return duration;
	}

	public float ChangeDuration(float multiplier, float diff)
	{
		duration *= multiplier;
		duration += diff;
		return duration;
	}
}


