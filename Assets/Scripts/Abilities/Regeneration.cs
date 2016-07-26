using UnityEngine;
using System.Collections;

public class Regeneration : MonoBehaviour {

	private EnemyHealth enemyHealth;

	public bool isRegenerating { get; set;}
	public ParticleSystem regenerationParticles;
	public float ratePerSecond = 0.05f;
	private float messageRate = 10.0f;

	void Start() {
		enemyHealth = GetComponent<EnemyHealth> ();
		if (enemyHealth == null) {
			this.enabled = false;		
		}
	}

	void Update() {
		if (enemyHealth.hasWounds && !isRegenerating) {
			isRegenerating = true;
			StartCoroutine (Regenerate ());
		}
	}

	IEnumerator Regenerate() {
		while (enemyHealth.hasWounds) {
			yield return new WaitForSeconds (messageRate);
			enemyHealth.changeCurrentHealth(ratePerSecond * messageRate);
			regenerationParticles.Play ();
		}
		StopCoroutine (Regenerate ());
		isRegenerating = false;
	}
}
