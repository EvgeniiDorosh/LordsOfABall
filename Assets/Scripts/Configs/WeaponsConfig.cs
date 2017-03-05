using System;

public class WeaponsConfig : CreaturesConfig<WeaponsConfig.Parameters> 
{
	[Serializable]
	public class Parameters  {

		public string name;

		public float attack;
		public float damage;
		public float firerate;
		public float speed;
		public float defenseIgnoring;
	}
}
