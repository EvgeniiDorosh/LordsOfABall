using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject paddle;
	public GameObject clonePaddle;

	void Awake () {
		clonePaddle = Instantiate (paddle, Vector2.zero, Quaternion.identity) as GameObject;
		AddListeners ();
	}

	void AddListeners() {
		EventManager.StartListening (LevelEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
	}

	void RemoveListeners() {
		EventManager.StopListening (LevelEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
	}
	
	void Update () {
	
	}

	void OnDestroy() {
		RemoveListeners ();
	}

	void CheckAllBallAreDestroyed() {
		if (Ball.balls.Count == 0) {
			clonePaddle.GetComponent<Paddle> ().launcher.Invoke("SetupBall", 0.5f);		
		}
	}
}
