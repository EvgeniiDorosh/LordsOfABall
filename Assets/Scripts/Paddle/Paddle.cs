using UnityEngine;

public class Paddle : MonoBehaviour 
{	
	BallLauncher launcher;
	public BallLauncher Launcher 
	{
		get { return launcher; }
	}

	void Awake () 
	{		
		launcher = GetComponentInChildren<BallLauncher> ();
	}
}
