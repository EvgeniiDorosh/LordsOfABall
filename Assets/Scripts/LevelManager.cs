using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public GameObject paddle;
	private GameObject clonePaddle;

	public List<GameObject> listThatMustBeDestroyed;
	private List<int> idsList = new List<int>();

	void Awake () {
		clonePaddle = Instantiate (paddle, Vector2.zero, Quaternion.identity) as GameObject;
		/*foreach (GameObject creature in listThatMustBeDestroyed) {
			idsList.Add (creature.GetInstanceID ());
		}*/
	}

	void OnEnable() {
		AddListeners ();
	}

	void OnDisable() {
		RemoveListeners ();
	}

	void AddListeners() {
		Messenger.AddListener(BallEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
		Messenger<GameObject>.AddListener (EnemyEvent.enemyWasDestroyed, CheckForWin);
	}

	void RemoveListeners() {
		Messenger.RemoveListener(BallEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
		Messenger<GameObject>.RemoveListener (EnemyEvent.enemyWasDestroyed, CheckForWin);
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

	void CheckForWin(GameObject instanceID) {
		/*idsList.Remove (instanceID);
		print (idsList.Count);
		if (idsList.Count == 0) {
			ShowWin ();
		}*/
		listThatMustBeDestroyed.Remove (instanceID);
		print (listThatMustBeDestroyed.Count);
		if (listThatMustBeDestroyed.Count == 0) {
			ShowWin ();
		}

	}

	void ShowWin() {
		Debug.Log ("You have won!");
	}
}
