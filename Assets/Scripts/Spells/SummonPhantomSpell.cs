using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class SummonPhantomSpell : Spell {

	[Range(1, 8)]
	public int ballsAmount = 1;

	void Start() {
		temporalType = TemporalType.Single;
	}

	override public void Cast() {
		int existedBallsCount = Ball.balls.Count;
		if (existedBallsCount > 0) {
			int index = Random.Range (0, existedBallsCount);
			GameObject ball = Ball.balls [index];
			Vector2 position = ball.transform.position;
			for (int i = 0; i < ballsAmount; i++) {				
				GameObject phantomBall = Instantiate(ball, position, Quaternion.identity) as GameObject;
				phantomBall.GetComponent<BallMotionController> ().Launch (Random.insideUnitCircle.normalized);
			}
		}
	}

	override public void Finish() {	}
}
