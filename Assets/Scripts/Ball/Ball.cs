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
		balls.Add (this.gameObject);
		paddle = GameObject.FindGameObjectWithTag ("Player").GetComponent<Paddle> ();
		InitialParameters = paddle.InitialParameters.Clone();
		CurrentParameters = paddle.CurrentParameters.Clone ();
		audioSource = GetComponent<AudioSource> ();
		base.Awake ();
	}

	override protected void InitControllers() {
		base.InitControllers ();
		ParamsController.destinationEvent = BallEvent.parameterWasUpdated;
		motionController = GetComponent<BallMotionController> ();
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

		ParamsController.ChangeParameter ("Attack", 1.0f);
		Debug.Log("Ball " + GetInstanceID() + " Attack = " + CurrentParameters.Attack);
	}

	public void Demolish()  {
		balls.Remove (this.gameObject);
		Messenger.Invoke(BallEvent.ballWasDestroyed);
		Destroy (gameObject);
	}
}
