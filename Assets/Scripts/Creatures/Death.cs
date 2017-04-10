using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour 
{
	[SerializeField]
	GameObject effect;

	Pool effectPool;

	IDestructible destructible;

	void Awake()
	{
		destructible = GetComponent<IDestructible> ();
		if (destructible != null)
			destructible.Destructed += ShowDeath;

		effectPool = ObjectPooler.CreatePool (new PoolVO (effect, 1));
	}
	
	void ShowDeath(GameObject target)
	{
		ShowingEffect showingEffect = effectPool.SpawnObject (target.transform.position).GetComponent<ShowingEffect>();
		showingEffect.Show ();
	}
}
