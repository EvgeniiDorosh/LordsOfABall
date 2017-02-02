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
			Destroy (gameObject);
			return;
		}
		instance = this;
		clonePaddle = Instantiate (paddle, Vector2.zero, Quaternion.identity) as GameObject;
		Cursor.visible = false;
		//Time.timeScale = 0.5f;
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
		int ballsCount = MembersAccount.Count (Member.Ball);
		if (ballsCount > 0) {
			List<GameObject> balls = MembersAccount.Get (Member.Ball);
			for (int index = 0; index < ballsCount; index++) {
				balls[index].GetComponent<BallMotionController> ().Stop ();
			}
		}
	}

	IEnumerator CompletingLevel() {
		StopPaddle ();
		StopBalls ();
		while (MembersAccount.Count (Member.Ball) != 0) {
			Ball ball = MembersAccount.Get (Member.Ball) [0].GetComponent<Ball> ();
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
