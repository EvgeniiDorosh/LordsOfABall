using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SummonPhantomItem : CollectableItem {

	public GameObject ball;

	void Start () {
	
	}

	override public void Trigger(GameObject targetObject) {
		if (Ball.balls.Count > 0) {
			Vector2 position = Ball.balls [0].transform.position;
			GameObject phantomBall = Instantiate (ball, position, Quaternion.identity) as GameObject;
			phantomBall.GetComponent<BallMotionController> ().Launch (Random.insideUnitCircle.normalized);
		}
	}
}
