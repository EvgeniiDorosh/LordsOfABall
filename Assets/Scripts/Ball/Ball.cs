using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour {
	
	public static readonly List<GameObject> balls = new List<GameObject>();

	private AudioSource audioSource;
	public AudioClip knockSound;

	public Death death;

	private ObjectPooler objectPooler;

	void Awake () {		
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = knockSound;
		balls.Add (this.gameObject);
	}

	void Start() {
		objectPooler = ObjectPooler.GetPool (PooledType.BallKnockLight);
	}

	void OnCollisionEnter2D(Collision2D other) {
		audioSource.Play ();
		Vector2 hitPoint = other.contacts[0].point;
		objectPooler.SpawnObject (hitPoint);
	}

	public void Demolish()  {
		balls.Remove (this.gameObject);
		Messenger.Invoke(BallEvent.ballWasDestroyed);
		if (death != null) {
			death.ShowDeath ();
		}
		Destroy (gameObject);
	}
}
