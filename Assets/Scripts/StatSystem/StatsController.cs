using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatsController : BasicStatsController
{
	bool isChecking = false;
	float checkStep = 1f;
	List<StatModifier> temporalModifiers = new List<StatModifier> ();

	public virtual void AddModifier(StatModifier modifier)
	{
		Stat stat = statsSet.Get<Stat> (modifier.TargetType);
		if (stat != null) 
		{
			stat.AddModifier (modifier);
			if (modifier.Duration < Mathf.Infinity) 
			{
				temporalModifiers.Add (modifier);
				if (!isChecking)
					StartCoroutine (UpdateModifiers ());
			}
		}			
	}

	public virtual void RemoveModifier(StatModifier modifier)
	{
		Stat stat = statsSet.Get<Stat> (modifier.TargetType);
		if (stat != null)
			stat.RemoveModifier (modifier);
	}

	public virtual void RemoveModifier(StatType statType, StatModifierType modifierType)
	{
		Stat stat = statsSet.Get<Stat> (statType);
		if (stat != null)
			stat.RemoveModifier (modifierType);
	}

	public virtual void ClearModifiers(StatType statType)
	{
		Stat stat = statsSet.Get<Stat> (statType);
		if (stat != null)
			stat.ClearModifiers ();
	}

	public virtual void ClearModifiers()
	{
		foreach (var stat in stats) 
			stat.ClearModifiers ();
	}

	IEnumerator UpdateModifiers() 
	{
		StatModifier modifier;
		while (temporalModifiers.Count > 0) 
		{
			yield return new WaitForSeconds (checkStep);
			for (int i = temporalModifiers.Count - 1; i >= 0; i--) 
			{
				modifier = temporalModifiers [i];
				if (modifier.ChangeDuration (-checkStep) <= 0) 
				{
					temporalModifiers.Remove (modifier);
					RemoveModifier (modifier);
				}
			}
		}
		StopAllCoroutines ();
	}

	void OnDestroy()
	{
		StopAllCoroutines ();
	}
}

