using UnityEngine;
using System.Collections;

public class DamageableDeath : MonoBehaviour {

	public GameObject deathParticles;
	public AudioClip deathSound;

	public void ShowDeath() {
		GameObject deathTrail = Instantiate(deathParticles, transform.position, Quaternion.identity) as GameObject;
		deathTrail.GetComponent<ParticleSystem> ().Play ();
		AudioSource audioSource = deathTrail.GetComponent<AudioSource> ();
		audioSource.clip = deathSound;
		audioSource.Play ();
	}
}
