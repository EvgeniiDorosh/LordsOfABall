using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

[Serializable]
public class CreatureParameters : Parameters {
	
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

	/**
	 * Damage
	 */

	[SerializeField]
	[Range(0, 500)]
	private float minimumDamage;

	[SerializeField]
	[Range(0, 500)]
	private float maximumDamage;

	public float MinimumDamage {
		get {
			if (minimumDamage < maximumDamage) {
				return minimumDamage < 0 ? 0 : minimumDamage;
			} else {
				return maximumDamage;
			}
		}
		set {
			minimumDamage = value;
		}
	}

	public float MaximumDamage {
		get {
			return maximumDamage < 0 ? 0 : maximumDamage;
		}
		set { 
			maximumDamage = value;
		}
	}

	/**
	 * Initiative
	 */

	[SerializeField]
	[Range(0, 100)]
	private float initiative;

	public float Initiative {
		get {
			return initiative < 0 ? 0 : initiative;
		}
		set { 
			initiative = value;
		}
	}

	/**
	 * Mana
	 */

	[SerializeField]
	[Range(0, 200)]
	private float mana;

	public float Mana {
		get {
			return mana < 0 ? 0 : mana;
		}
		set { 
			mana = value;
		}
	}

	/**
	* Spell power 
	*/

	[SerializeField]
	[Range(0, 200)]
	private float spellPower;

	public float SpellPower {
		get {
			return spellPower < 0 ? 0 : spellPower;
		}
		set { 
			spellPower = value;
		}
	}

	/**
	 * Luck
	 */ 
	[SerializeField]
	[Range(0, 100)]
	private float luck;

	public float Luck {

		get { 
			return luck;
		}
		set { 
			luck = value;		
		}
	}

	/**
	 * Morale
	 */ 
	[SerializeField]
	[Range(0, 100)]
	private float morale;

	public float Morale {

		get { 
			return morale;
		}
		set { 
			morale = value;		
		}
	}

	public CreatureParameters Clone() {
		CreatureParameters result = new CreatureParameters ();

		PropertyInfo[] properties = GetType ().GetProperties ();
		foreach(PropertyInfo property in properties) {
			property.SetValue(result, property.GetValue(this, null), null);
		}

		return result;
	}
}
