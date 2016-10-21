using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public static LevelManager instance = null;

	public GameObject paddle;
	private GameObject clonePaddle;

	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		clonePaddle = Instantiate (paddle, Vector2.zero, Quaternion.identity) as GameObject;
	}

	void OnEnable() {
		AddListeners ();
	}

	void OnDisable() {
		RemoveListeners ();
	}

	void AddListeners() {
		Messenger.AddListener (LevelEvent.allEnemiesAreDestroyed, OnLevelComplete);
	}

	void RemoveListeners() {
		Messenger.RemoveListener (LevelEvent.allEnemiesAreDestroyed, OnLevelComplete);
	}

	void OnLevelComplete() {
		StopPaddle ();
		StopBalls ();
		StartCoroutine (DestroyBalls ());
	}

	void StopPaddle() {
		clonePaddle.GetComponentInChildren<PaddleMotionController> ().enabled = false;
		clonePaddle.GetComponentInChildren<BallLauncher> ().enabled = false;
	}

	void StopBalls() {
		foreach (GameObject ball in Ball.balls) {
			ball.GetComponent<BallMotionController> ().Stop ();
		}
	}

	IEnumerator DestroyBalls() {
		while (Ball.balls.Count != 0) {
			Ball ball = Ball.balls [0].GetComponent<Ball> ();
			ball.Demolish ();
			yield return new WaitForSeconds (0.5f);
		}
		StopCoroutine ("DestroyBalls");
		yield return new WaitForSeconds (2f);
		Messenger.Invoke (LevelEvent.levelIsComplete);
	}
}
