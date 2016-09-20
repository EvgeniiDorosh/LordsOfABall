using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Ball : Attacker {
	
	public static readonly List<GameObject> balls = new List<GameObject>();

	private Paddle paddle;

	private BallMotionController motionController;
	public BallMotionController MotionController {
		get { 
			return motionController;
		}
	}

	private AudioSource audioSource;
	public AudioClip knockSound;

	public GameObject knockLight;

	new void Awake () {
		base.Awake ();
		paddle = GameObject.FindGameObjectWithTag ("Player").GetComponent<Paddle> ();
		ParamsController.InitialParameters = paddle.InitialParameters.Clone();
		ParamsController.CurrentParameters = paddle.CurrentParameters.Clone ();
		ParamsController.destinationEvent = BallEvent.parameterWasUpdated;
		motionController = GetComponent<BallMotionController> ();
		audioSource = GetComponent<AudioSource> ();

		balls.Add (this.gameObject);
	}

	void OnEnable() {
		Messenger<StatChange>.AddListener (PaddleEvent.parameterWasUpdated, OnPaddleParameterUpdate);
	}

	void OnDisable(){
		Messenger<StatChange>.RemoveListener (PaddleEvent.parameterWasUpdated, OnPaddleParameterUpdate);
	}

	void OnPaddleParameterUpdate(StatChange statChange) {
		ParamsController.ChangeParameter (statChange.name, statChange.diff);
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
		Destroy (gameObject);
	}
}
