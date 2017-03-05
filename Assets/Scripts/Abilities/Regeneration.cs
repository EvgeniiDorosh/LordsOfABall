using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Damageable))]
public class Regeneration : MonoBehaviour 
{
	Damageable target; 

	[SerializeField]
	ParticleSystem regenerationParticles;

	public float ratePerSecond = 0.05f;
	float messageRate = 10.0f;
	WaitForSeconds waiting = null;

	public bool isRegenerating { get; set;}

	void Start() 
	{
		target = GetComponent<Damageable> ();
		waiting = new WaitForSeconds (messageRate);
	}

	void Update() 
	{
		if (!isRegenerating && target.HasWounds) 
		{
			isRegenerating = true;
			StartCoroutine (Regenerate ());
		}
	}

	IEnumerator Regenerate() 
	{
		while (target.HasWounds) 
		{
			yield return waiting;
			target.ChangeStatValue(StatType.CurrentHealth, ratePerSecond * messageRate);
			regenerationParticles.Play ();
		}
		Stop ();
	}

	void Stop()
	{
		StopAllCoroutines ();
		isRegenerating = false;
	}

	void OnDisable()
	{
		Stop ();
	}
}
