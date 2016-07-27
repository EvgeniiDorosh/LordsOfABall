using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public BallLauncher launcher;

	void Awake () {
		launcher = GetComponentInChildren<BallLauncher> ();
	}
	
	void FixedUpdate () {
		

	}
}
