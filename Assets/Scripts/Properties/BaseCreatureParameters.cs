using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseCreatureParameters {
	
	/**
	 * Health
	 */

	[SerializeField]
	[Range(0, 500)]
	private float health;

	public float Health {
		get {
			return health < 0 ? 0 : health;		
		}
		set {
			health = value;
		}
	}

	/**
	 * Attack
	 */

	[SerializeField]
	[Range(0, 500)]
	private float attack;

	public float Attack {
		get {
			return attack < 0 ? 0 : attack;		
		}
		set {
			attack = value;
		}
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
	 * Mana
	 */

	[SerializeField]
	[Range(0, 500)]
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
	[Range(0, 500)]
	private float spellPower;

	public float SpellPower {
		get {
			return spellPower < 0 ? 0 : spellPower;
		}
		set { 
			spellPower = value;
		}
	}

	public BaseCreatureParameters Clone() {
		BaseCreatureParameters result = new BaseCreatureParameters ();
		result.health = this.health;
		result.attack = this.attack;
		result.defense = this.defense;
		result.minimumDamage = this.minimumDamage;
		result.maximumDamage = this.maximumDamage;
		result.mana = this.mana;
		result.spellPower = this.spellPower;

		return result;
	}
}
