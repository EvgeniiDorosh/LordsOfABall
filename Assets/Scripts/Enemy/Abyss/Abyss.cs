using UnityEngine;
using System.Collections;

public class Abyss : MonoBehaviour, IAttacker {

	private bool isReflexive;
	private BoxCollider2D boxCollider;

	public bool IsReflexive {
		get { return isReflexive; }
		set { 
			isReflexive = value;
			boxCollider.isTrigger = !value;
		}
	}

	private AbyssParametersController paramsController;
	private IDamageable paddle;

	public float GetDamage (ICreature creature) {
		float resultDamage;
		float pureDamage = GetCurrentValue("Damage");
		float attack = GetCurrentValue("Attack");
		float defense = creature.GetCurrentValue("Defense");

		if (attack > defense) {
			resultDamage = pureDamage * (1 + 0.05f * (attack - defense));
		} else {
			resultDamage = pureDamage / (1 + 0.05f * (defense - attack));
		}

		return resultDamage;
	}

	public float GetCurrentValue (string paramName) {
		return paramsController.Parameters.GetValue(paramName);
	}

	public float GetInitialValue (string paramName) {
		return paramsController.Parameters.GetValue(paramName);
	}

	public void ChangeParameter (string paramName, float diffValue) {
		paramsController.ChangeParameter (paramName, diffValue);
	}

	void Awake() {
		boxCollider = GetComponent<BoxCollider2D> ();
		IsReflexive = false;
		paddle = GameObject.FindGameObjectWithTag ("Player").GetComponent<IDamageable> ();
	}

	void OnEnable () {
		paramsController = GetComponent<AbyssParametersController> ();
		paramsController.Parameters = new AbyssParameters();
		paramsController.Parameters.SetValue ("Damage", 1f);
		Messenger<GameObject>.AddListener (CreatureEvent.creatureWasDestroyed, OnCreatureDestroy);
	}

	void OnDisable() {
		Messenger<GameObject>.RemoveListener (CreatureEvent.creatureWasDestroyed, OnCreatureDestroy);
	}

	void OnCreatureDestroy(GameObject target) {
		ICreature creature = target.GetComponent<ICreature> ();
		if (creature != null) {
			float creatureHealth = creature.GetInitialValue ("Health");
			float attackIncrease = Mathf.Log (creatureHealth);
			ChangeParameter ("Attack", attackIncrease);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherGameObject = other.gameObject;
		if (otherGameObject.CompareTag ("Ball")) {
			if (MembersAccount.Count(Member.Ball) == 1) {
				paddle.ApplyDamage (this);
			}
			otherGameObject.GetComponent<Ball>().Demolish();
		}
	}
}
