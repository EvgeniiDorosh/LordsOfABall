using System;
using System.Collections.Generic;

public static class StatsConfig
{
	static Dictionary<string, StatType> statTypes;

	static StatsConfig ()
	{
		statTypes = GetTypes ();
	}

	static Dictionary<string, StatType> GetTypes()
	{
		Dictionary<string, StatType> result = new Dictionary<string, StatType> ();
		Array items = Enum.GetValues (typeof(StatType));
		foreach (var item in items) 
		{
			string name = item.ToString ();
			name = Char.ToLower (name [0]) + name.Substring (1);
			result.Add (name, (StatType)item);
		}

		return result;
	}

	public static StatType GetType(string name)	
	{
		if (statTypes.ContainsKey (name)) 
		{
			return statTypes [name];
		}

		return StatType.None;
	}
}
