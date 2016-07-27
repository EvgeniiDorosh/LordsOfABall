using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour {

	public static readonly List<GameObject> balls = new List<GameObject>();

	private const float TORQUE = 20.0f;

	public float initialVelocity = 10.0f;
	public float velocity;

	private Rigidbody2D rigitbody;

	private AudioSource audioSource;
	public AudioClip knockSound;

	public GameObject knockLight;

	void Awake () {
		balls.Add (this.gameObject);
		rigitbody = GetComponent<Rigidbody2D> ();
		audioSource = GetComponent<AudioSource> ();
		velocity = velocity > 0 ? velocity : initialVelocity;
	}
	
	void Update () {
		
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
		EventManager.TriggerEvent (LevelEvent.ballWasDestroyed);
	}

	public void Launch(Vector2 direction) {
		transform.parent = null;
		rigitbody.isKinematic = false;
		rigitbody.velocity = direction * velocity;
	}

	public void Stop() {
		rigitbody.isKinematic = true;
		rigitbody.velocity = Vector2.zero;
	}

	public void ChangeVelocity(float diff) {
		velocity = velocity + diff;
		rigitbody.velocity += rigitbody.velocity.normalized * diff;
	}

	IEnumerator SpeedIncreasing() {
		for (;;) {
			ChangeVelocity (0.1f);
			yield return new WaitForSeconds (0.5f);
		}
	}	
}
