using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class BaseStat 
{
	[SerializeField]
	protected StatType type;
	[SerializeField]
	protected float value;

	#region Constructors

	public BaseStat()
	{
		this.type = StatType.None;
		this.value = 0;
	}

	public BaseStat (StatType type, float value)
	{
		this.type = type;
		this.value = value;
	}

	#endregion

	public delegate void ValueChangedHandler(BaseStat stat);
	public event ValueChangedHandler ValueChanged;

	public StatType Type
	{
		get { return type;}
	}

	public virtual float Value
	{
		get { return value;}
		set 
		{ 
			if (this.value != value) 
			{
				this.value = value;
				OnValueChanged ();
			}
		}
	}

	public float RawValue
	{
		get { return value;}
	}

	override public string ToString()
	{
		return string.Format ("Type = {0}, value = {1}", Type, Value);
	}

	protected virtual void OnValueChanged()
	{
		ValueChangedHandler handler = ValueChanged;
		if (handler != null) 
		{
			handler (this);
		}
	}
}

