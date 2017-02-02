using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Damageable))]
public class Regeneration : MonoBehaviour 
{
	Damageable target; 

	public bool isRegenerating { get; set;}

	[SerializeField]
	ParticleSystem regenerationParticles;
	public float ratePerSecond = 0.05f;

	float messageRate = 10.0f;
	WaitForSeconds waiting = null;

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
			target.ChangeParameter("Health", ratePerSecond * messageRate);
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
