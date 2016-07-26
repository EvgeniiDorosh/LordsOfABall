using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject paddle;

	void Awake () {
		Instantiate (paddle, Vector2.zero, Quaternion.identity);
		EventManager.StartListening (LevelEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
	}

	void AddListeners() {
		
	}

	void RemoveListeners() {
	
	}
	
	void Update () {
	
	}

	void OnDestroy() {
	
	}

	void CheckAllBallAreDestroyed() {
		Debug.Log ("Trigger");
	}
}
