using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public class BallMotionController : MonoBehaviour {
	
	private float initialSpeed;
	public float InitialSpeed {
		get { return initialSpeed;}
		set { initialSpeed = value;}
	}

	private float currentSpeed;
	public float CurrentSpeed {
		get { return currentSpeed; }
		set { currentSpeed = value; }
	}

	private Rigidbody2D rigidBody;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void OnEnable () {
		Messenger<StatChange>.AddListener (PaddleEvent.speedWasUpdated, OnSpeedChanged);
	}

	void OnDisable () { 
		Messenger<StatChange>.RemoveListener (PaddleEvent.speedWasUpdated, OnSpeedChanged);
	}

	void OnSpeedChanged(StatChange speedChange) {
		switch (speedChange.name) {
		case "Speed":
			ChangeSpeed (speedChange.diff, 4);
			break;
		case "InitialSpeed":
			initialSpeed = speedChange.value;
			break;
		}
	}

	public void ChangeSpeed(float diff) {
		currentSpeed += diff;
		UpdateSpeed ();
	}

	public void ChangeSpeed(float diff, uint time) {
		StopCoroutine ("SpeedIncreasing");
		float newSpeed = currentSpeed + diff;
		StartCoroutine (SpeedIncreasing (newSpeed, time));
	}

	public void ChangeSpeed(Vector2 direction) {
		rigidBody.velocity = direction * currentSpeed;
	}

	void UpdateSpeed() {
		rigidBody.velocity = rigidBody.velocity.normalized * currentSpeed;
	}

	public void Launch(Vector2 direction) {
		transform.parent = null;
		rigidBody.isKinematic = false;
		ChangeSpeed(direction);
	}

	public void Stop() {
		rigidBody.isKinematic = true;
		rigidBody.velocity = Vector2.zero;
	}

	IEnumerator SpeedIncreasing(float targetSpeed, uint time) {
		float timeStep = 0.5f;
		float stepValue = Mathf.Abs (targetSpeed - currentSpeed) * timeStep / time;
		while (currentSpeed != targetSpeed) {
			currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, stepValue);
			UpdateSpeed ();
			yield return new WaitForSeconds (timeStep);
		}
		StopCoroutine ("SpeedIncreasing");
	}	
}
