using UnityEngine;
using System.Collections;

public class BallImpulse : MonoBehaviour {

	private BallMotionController ballMotionController;
	private float duration = 1f;
	private float diff;

	void Awake() {
		ballMotionController = GetComponent<BallMotionController> ();
		diff = ballMotionController.CurrentSpeed;
	}

	void Start () {		
		ballMotionController.ChangeSpeed (diff);
		Invoke ("Stop", duration);
	}
	
	void Stop () {
		ballMotionController.ChangeSpeed (-diff, 1);
		Destroy (this, 2f);
	}
}
