using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

public class LevelManager : MonoBehaviour {

	public GameObject paddle;
	private GameObject clonePaddle;

	void Awake () {
		clonePaddle = Instantiate (paddle, Vector2.zero, Quaternion.identity) as GameObject;
	}

	void OnEnable() {
		AddListeners ();
	}

	void OnDisable() {
		RemoveListeners ();
	}

	void AddListeners() {
		Messenger.AddListener(BallEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
	}

	void RemoveListeners() {
		Messenger.RemoveListener(BallEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
	}
	
	void Update () {
	
	}

	void CheckAllBallAreDestroyed() {
		if (Ball.balls.Count == 0) {
			Paddle paddle = clonePaddle.GetComponent<Paddle> ();
			//paddle.paramsController.ChangeHealth (-1f);// TODO Uncomment
			paddle.Launcher.Invoke("SetupBall", 0.5f);		
		}
	}
}
