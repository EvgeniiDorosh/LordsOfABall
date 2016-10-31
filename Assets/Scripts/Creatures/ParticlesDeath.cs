using UnityEngine;
using System.Collections;

public class ParticlesDeath : Death {

	private ParticleSystem deathTrail;
	private AudioSource audioSource;
	private Transform initialParent;

	void Awake() {
		deathTrail = GetComponent<ParticleSystem>();
		audioSource = GetComponent<AudioSource>();
		initialParent = transform.parent;
	}

	override public void ShowDeath() {
		transform.parent = null;
		deathTrail.Play ();
		audioSource.Play ();
		Invoke ("Hide", 2f);
	}

	private void Hide() {
		CancelInvoke ();
		if (initialParent != null) {
			transform.parent = initialParent;
		} else {
			Destroy (gameObject);
		}
	}
}
