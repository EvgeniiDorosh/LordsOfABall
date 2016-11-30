using UnityEngine;
using System;

[Serializable]
public class AbyssParameters : Parameters {

	/**
	 * Attack
	 */

	[SerializeField]
	[Range(0, 500)]
	private float attack;

	public float Attack { 
		get { return attack < 0 ? 0 : attack; }
		set { attack = value; }
	}

	/**
	 * Damage
	 */

	[SerializeField]
	[Range(0, 500)]
	private float damage;

	public float Damage {
		get { return damage;}
		set { damage = value; }
	}

	public AbyssParameters Clone() {
		AbyssParameters result = new AbyssParameters ();
		result.Attack = attack;
		result.Damage = damage;
		return result;
	}
}
