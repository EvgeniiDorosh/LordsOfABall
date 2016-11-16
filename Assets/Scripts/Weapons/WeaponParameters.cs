using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class WeaponParameters : Parameters {

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
		get { return damage < 0 ? 0 : damage; }
		set { damage = value; }
	}

	/**
	 * Firerate / per minute
	 */

	[SerializeField]
	[Range(0, 500)]
	private float firerate;

	public float Firerate {
		get { return firerate < 0 ? 0 : (60f / firerate); }
		set { firerate = value; }
	}

	/**
	 * Speed
	 */

	[SerializeField]
	[Range(0, 100)]
	private float speed;

	public float Speed {
		get { return speed < 0 ? 0 : speed; }
		set { speed = value; }
	}

	/**
	 * Defense ignoring
	 */

	[SerializeField]
	[Range(0, 100)]
	private float defenseIgnoring;

	public float DefenseIgnoring {
		get { return defenseIgnoring < 0 ? 0 : defenseIgnoring; }
		set { defenseIgnoring = value; }
	}

	public WeaponParameters Clone() {
		WeaponParameters result = new WeaponParameters ();
		result.Attack = attack;
		result.Damage = damage;
		result.Firerate = firerate;
		result.Speed = speed;
		result.DefenseIgnoring = defenseIgnoring;

		return result;
	}
}
