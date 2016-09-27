using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
	
	private BallLauncher launcher;
	public BallLauncher Launcher {
		get { return launcher; }
	}

	void Awake () {		
		launcher = GetComponentInChildren<BallLauncher> ();
	}
}
