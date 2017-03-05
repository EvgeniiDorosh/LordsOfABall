using System;
using System.Reflection;
using System.Collections.Generic;

public class PaddleConfig : CreaturesConfig<PaddleConfig.Parameters>
{
	[Serializable]
	public class Parameters
	{
		public float attack;
		public float defense;
		public float minimumDamage;
		public float maximumDamage;
		public float initiative;
		public float health;
		public float mana;
		public float spellPower;
		public float luck;
		public float morale;

		public float speed;
		public float width;
	}

	override public void Initialize()
	{
		FieldInfo[] fields;
		PaddleConfig.Parameters paddleParams = parameters[0];
		fields = paddleParams.GetType().GetFields (BindingFlags.Public | BindingFlags.Instance);
		List<StatBlank> statsBlanks = new List<StatBlank>();
		foreach (FieldInfo field in fields) 
		{
			StatType statType = StatsConfig.GetType (field.Name);
			if (statType != StatType.None) 
			{
				statsBlanks.Add (new StatBlank { type = statType, value = (float)field.GetValue (paddleParams) });
			}
		}

		blanks.Add("Paddle", statsBlanks);
	}
}
