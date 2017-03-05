using UnityEngine;
using System.Collections;

public class Abyss : MonoBehaviour, IAttacker 
{
	private bool isReflexive;
	private BoxCollider2D boxCollider;

	public bool IsReflexive 
	{
		get { return isReflexive; }
		set 
		{ 
			isReflexive = value;
			boxCollider.isTrigger = !value;
		}
	}

	private BasicStatsController statsController;
	private IDamageable paddle;

	public float GetStatValue(StatType type)
	{
		return statsController.Get<BaseStat> (type).Value;
	}

	public void ChangeStatValue (StatType type, float diffValue) 
	{
		statsController.ChangeValue<BaseStat>(type, diffValue);
	}

	public float GetDamage (ICreature creature) 
	{
		float resultDamage;
		float pureDamage = GetStatValue(StatType.Damage);
		float attack = GetStatValue(StatType.Attack);
		float defense = creature.GetStatValue(StatType.Defense);

		if (attack > defense) 
		{
			resultDamage = pureDamage * (1 + 0.05f * (attack - defense));
		}
		else 
		{
			resultDamage = pureDamage / (1 + 0.05f * (defense - attack));
		}

		return resultDamage;
	}

	void Awake() 
	{
		boxCollider = GetComponent<BoxCollider2D> ();
		IsReflexive = false;
	}

	void OnEnable () 
	{
		statsController = GetComponent<BasicStatsController> ();
		statsController.Add(new BaseStat(StatType.Attack, 0));
		statsController.Add (new BaseStat (StatType.Damage, 1f));
		Messenger<GameObject>.AddListener (CreatureEvent.creatureWasDestroyed, OnCreatureDestroy);
	}

	void Start()
	{
		paddle = GameObject.FindGameObjectWithTag ("Player").GetComponent<IDamageable> ();
	}

	void OnDisable() 
	{
		Messenger<GameObject>.RemoveListener (CreatureEvent.creatureWasDestroyed, OnCreatureDestroy);
	}

	void OnCreatureDestroy(GameObject target) 
	{
		ICreature creature = target.GetComponent<ICreature> ();
		if (creature != null) 
		{
			float creatureHealth = creature.GetStatValue (StatType.Health);
			float attackIncrease = Mathf.Log (creatureHealth);
			ChangeStatValue (StatType.Attack, attackIncrease);
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
