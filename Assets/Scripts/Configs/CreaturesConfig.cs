using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class CreaturesConfig<T> where T : class
{
	public T[] parameters;

	protected readonly Dictionary<string, List<StatBlank>> blanks = new Dictionary<string, List<StatBlank>> ();

	public virtual void Initialize()
	{
		FieldInfo[] fields;
		FieldInfo name;
		Type type = typeof(T);
		fields = type.GetFields (BindingFlags.Public | BindingFlags.Instance);
		name = type.GetField ("name");

		foreach (var parameter in parameters) 
		{
			List<StatBlank> statsBlanks = new List<StatBlank>();
			foreach (FieldInfo field in fields) 
			{
				StatType statType = StatsConfig.GetType (field.Name);
				if (statType != StatType.None) 
				{
					statsBlanks.Add (new StatBlank { type = statType, value = (float)field.GetValue (parameter) });
				}
			}

			blanks.Add ((string)name.GetValue(parameter), statsBlanks);
		}
	}

	public List<StatBlank> GetBlanks(string name)
	{
		if (blanks.ContainsKey (name)) 
		{
			return blanks [name];
		}
		return null;
	}
}

