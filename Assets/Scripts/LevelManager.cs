using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject paddle;
	public GameObject clonePaddle;

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
			clonePaddle.GetComponent<Paddle> ().launcher.Invoke("SetupBall", 0.5f);		
		}
	}
}
