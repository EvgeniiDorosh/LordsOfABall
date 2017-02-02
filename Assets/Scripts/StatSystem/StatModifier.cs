using System;
using UnityEngine;

[Serializable]
public class StatModifier
{
	[SerializeField]
	StatModifierType effectType;
	public StatModifierType EffectType
	{
		get { return effectType;}
	}

	[SerializeField]
	StatType targetType;
	public StatType TargetType
	{
		get { return targetType;}
	}

	[SerializeField]
	float value;
	public float Value
	{
		get { return value;}	
	}

	public StatModifier (StatModifierType effectType, StatType targetType, float value)
	{
		this.effectType = effectType;
		this.targetType = targetType;
		this.value = value;
	}
}


