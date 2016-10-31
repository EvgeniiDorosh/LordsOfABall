using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	private static LevelManager instance = null;
	public static LevelManager Instance {
		get { return instance;}
	}

	public GameObject paddle;
	private GameObject clonePaddle;

	void Awake () {
		if (instance != null) {
			DestroyImmediate (gameObject);
			return;
		}
		instance = this;
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
		StartCoroutine (CompletingLevel ());
	}

	void StopPaddle() {
		clonePaddle.GetComponentInChildren<BallLauncher> ().enabled = false;
	}

	void StopBalls() {
		foreach (GameObject ball in Ball.balls) {
			ball.GetComponent<BallMotionController> ().Stop ();
		}
	}

	IEnumerator CompletingLevel() {
		StopPaddle ();
		StopBalls ();
		while (Ball.balls.Count != 0) {
			Ball ball = Ball.balls [0].GetComponent<Ball> ();
			ball.Demolish ();
			yield return new WaitForSeconds (0.5f);
		}
		while (InitialParameterSpell.createdInitialSpells.Count != 0) {
			yield return new WaitForSeconds (2f);		
		}
		yield return new WaitForSeconds (1f);
		StopAllCoroutines ();
		Messenger.Invoke (LevelEvent.levelIsComplete);
	}
}
