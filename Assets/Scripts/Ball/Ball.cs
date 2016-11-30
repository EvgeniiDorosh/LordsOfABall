using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour {
	
	private AudioSource audioSource;
	public AudioClip knockSound;

	public Death death;

	private ObjectPooler objectPooler;

	void Awake () {		
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = knockSound;
	}

	void Start() {
		objectPooler = ObjectPooler.GetPool (PooledType.BallKnockLight);
	}

	void OnEnable() {
		MembersAccount.Add (Member.Ball, gameObject);
	}

	void OnCollisionEnter2D(Collision2D other) {
		audioSource.Play ();
		Vector2 hitPoint = other.contacts[0].point;
		objectPooler.SpawnObject (hitPoint);
	}

	public void Demolish()  {
		if (death != null) {
			death.ShowDeath ();
		}
		Destroy (gameObject);
	}

	void OnDisable() {
		MembersAccount.Remove (Member.Ball,gameObject);
		Messenger.Invoke(BallEvent.ballWasDestroyed);
	}
}
