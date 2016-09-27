using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour {
	
	public static readonly List<GameObject> balls = new List<GameObject>();

	private AudioSource audioSource;
	public AudioClip knockSound;

	public GameObject knockLight;

	void Awake () {		
		audioSource = GetComponent<AudioSource> ();
		balls.Add (this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D other) {
		audioSource.clip = knockSound;
		audioSource.Play ();

		foreach(ContactPoint2D contactPoint in other.contacts) {
			Vector2 hitPoint = contactPoint.point;
			Instantiate(knockLight, new Vector2(hitPoint.x, hitPoint.y), Quaternion.identity);
		}
	}

	public void Demolish()  {
		balls.Remove (this.gameObject);
		Messenger.Invoke(BallEvent.ballWasDestroyed);
		GetComponent<DamageableDeath>().ShowDeath ();
		Destroy (gameObject);
	}
}
