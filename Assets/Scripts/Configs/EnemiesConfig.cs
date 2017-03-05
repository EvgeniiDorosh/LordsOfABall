using System;

public class EnemiesConfig : CreaturesConfig<EnemiesConfig.Parameters>
{
	[Serializable]
	public class Parameters
	{
		public string name;

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
	}
}
