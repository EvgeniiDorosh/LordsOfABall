using UnityEngine;
using System;

[Serializable]
public class DestructibleParameters : Parameters {

	/**
	 * Health
	 */

	[SerializeField]
	[Range(0, 1000)]
	private float health;

	public float Health {
		get { return health < 0 ? 0 : health; }
		set { health = value; }
	}

	/**
	 * Defense
	 */

	[SerializeField]
	[Range(0, 500)]
	private float defense;

	public float Defense {
		get {
			return defense < 0 ? 0 : defense;
		}
		set { 
			defense = value;
		}
	}

	public DestructibleParameters Clone() {
		DestructibleParameters result = new DestructibleParameters ();
		result.Health = health;
		result.Defense = defense;

		return result;
	}
}
