using UnityEngine;
using System.Collections;

public class Abyss : MonoBehaviour, IAttacker 
{
	bool isReflexive;
	BoxCollider2D boxCollider;

	BasicStatsController statsController;
	IDamageable paddle;

	WaitForSeconds attackIncreasingWait = new WaitForSeconds (20f);

	public bool IsReflexive 
	{
		get { return isReflexive; }
		set 
		{ 
			isReflexive = value;
			boxCollider.isTrigger = !value;
		}
	}

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
			resultDamage = pureDamage * (1 + 0.05f * (attack - defense));
		else 
			resultDamage = pureDamage / (1 + 0.05f * (defense - attack));

		return resultDamage;
	}

	void Awake() 
	{
		boxCollider = GetComponent<BoxCollider2D> ();
		IsReflexive = false;
	}

	void Start()
	{
		paddle = GameObject.FindGameObjectWithTag ("Player").GetComponent<IDamageable> ();
	}

	void OnEnable()
	{
		statsController = GetComponent<BasicStatsController> ();
		statsController.Add(new BaseStat(StatType.Attack, 0));
		statsController.Add (new BaseStat (StatType.Damage, 1f));
		StartCoroutine (AttackIncreasing());
	}

	void OnDisable()
	{
		StopAllCoroutines ();
	}

	IEnumerator AttackIncreasing()	
	{
		while (true) 
		{
			yield return attackIncreasingWait;
			ChangeStatValue (StatType.Attack, 1f);
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		GameObject otherGameObject = other.gameObject;
		if (otherGameObject.CompareTag ("Ball")) 
			if (MembersAccount.Count(Member.Ball) == 1)
				paddle.ApplyDamage (this);
		
		IDestructible destructible = otherGameObject.GetComponent<IDestructible> ();
		destructible.Destruct ();
	}
}