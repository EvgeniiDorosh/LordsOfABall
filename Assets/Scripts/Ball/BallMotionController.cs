using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public class BallMotionController : MonoBehaviour {

	private float defaultSpeed;

	private float currentSpeed;
	public float CurrentSpeed {
		get {
			return currentSpeed;
		}
		set {
			currentSpeed = value;
		}
	}

	private Rigidbody2D rigidBody;

	void Awake() {
		Paddle paddle = GameObject.FindGameObjectWithTag ("Player").GetComponent<Paddle>();
		defaultSpeed = paddle.DefaultBallSpeed;
		currentSpeed = defaultSpeed;
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void OnEnable () {
		Messenger<float>.AddListener (PaddleEvent.speedWasUpdated, OnDefaultSpeedChanged);
	}

	void OnDisable () { 
		Messenger<float>.RemoveListener (PaddleEvent.speedWasUpdated, OnDefaultSpeedChanged);
	}

	void UpdateSpeed() {
		rigidBody.velocity = rigidBody.velocity.normalized * currentSpeed;
	}

	void OnDefaultSpeedChanged(float defaultSpeed) {
		float diff = defaultSpeed - this.defaultSpeed;
		ChangeCurrentSpeed (diff);
	}

	public void ResetValue() {
		currentSpeed = defaultSpeed;
		UpdateSpeed ();
	}

	public void ChangeCurrentSpeed(float diff) {
		CurrentSpeed += diff;
		UpdateSpeed ();
	}

	public void Launch(Vector2 direction) {
		transform.parent = null;
		rigidBody.isKinematic = false;
		rigidBody.velocity = direction * CurrentSpeed;
	}

	public void Stop() {
		rigidBody.isKinematic = true;
		rigidBody.velocity = Vector2.zero;
	}

	IEnumerator SpeedIncreasing() {
		for (;;) {
			ChangeCurrentSpeed (.1f);
			yield return new WaitForSeconds (.5f);
		}
	}	
}
