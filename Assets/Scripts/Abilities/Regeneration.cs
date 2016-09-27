using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Damageable))]
public class Regeneration : MonoBehaviour {

	private Damageable target; 

	public bool isRegenerating { get; set;}
	public ParticleSystem regenerationParticles;
	public float ratePerSecond = 0.05f;
	private float messageRate = 10.0f;

	void Start() {
		target = GetComponent<Damageable> ();
	}

	void Update() {
		if (target.HasWounds && !isRegenerating) {
			isRegenerating = true;
			StartCoroutine (Regenerate ());
		}
	}

	IEnumerator Regenerate() {
		while (target.HasWounds) {
			yield return new WaitForSeconds (messageRate);
			target.ChangeParameter("Health", ratePerSecond * messageRate);
			regenerationParticles.Play ();
		}
		StopCoroutine (Regenerate ());
		isRegenerating = false;
	}
}
