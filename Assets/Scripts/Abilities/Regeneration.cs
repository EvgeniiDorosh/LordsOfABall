using UnityEngine;
using System.Collections;

public class Regeneration : MonoBehaviour {

	private HealthController healthController;

	public bool isRegenerating { get; set;}
	public ParticleSystem regenerationParticles;
	public float ratePerSecond = 0.05f;
	private float messageRate = 10.0f;

	void Start() {
		healthController = GetComponent<HealthController> ();
		if (healthController == null) {
			this.enabled = false;		
		}
	}

	void Update() {
		if (healthController.HasWounds && !isRegenerating) {
			isRegenerating = true;
			StartCoroutine (Regenerate ());
		}
	}

	IEnumerator Regenerate() {
		while (healthController.HasWounds) {
			yield return new WaitForSeconds (messageRate);
			healthController.ChangeCurrentHealth(ratePerSecond * messageRate);
			regenerationParticles.Play ();
		}
		StopCoroutine (Regenerate ());
		isRegenerating = false;
	}
}
