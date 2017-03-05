using System;

public class DestructiblesConfig : CreaturesConfig<DestructiblesConfig.Parameters> 
{
	[Serializable]
	public class Parameters
	{
		public string name;

		public float health;
		public float defense;
	}
}

