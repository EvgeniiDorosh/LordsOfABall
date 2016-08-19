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

	new protected void InitControllers() {
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
		switch (statChange.name) {
		case "Attack":
			ParamsController.ChangeAttack (statChange.diff);
			break;
		case "InitialAttack":
			ParamsController.ChangeInitialAttack (statChange.diff);
			break;
		case "MinimumDamage":
			ParamsController.ChangeMinimumDamage (statChange.diff);
			break;
		case "InitialMinimumDamage":
			ParamsController.ChangeInitialMinimumDamage (statChange.diff);
			break;
		case "MaximumDamage":
			ParamsController.ChangeMaximumDamage (statChange.diff);
			break;
		case "InitialMaximumDamage":
			ParamsController.ChangeInitialMaximumDamage (statChange.diff);
			break;
		case "Luck":
			ParamsController.ChangeLuck (statChange.diff);
			break;
		case "InitialLuck":
			ParamsController.ChangeInitialLuck (statChange.diff);
			break;
		default:
			break;
		}
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
