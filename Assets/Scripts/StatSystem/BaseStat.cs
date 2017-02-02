using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class BaseStat 
{
	[SerializeField]
	StatType type;
	public StatType Type
	{
		get { return type;}
	}
	[SerializeField]
	float value;
	public float Value
	{
		get { return value;}
		set { this.value = value;}
	}

	public BaseStat (StatType type, float value)
	{
		this.type = type;
		this.value = value;
	}

	override public string ToString()
	{
		return string.Format ("Type = {0}, value = {1}", type, value);
	}
}

