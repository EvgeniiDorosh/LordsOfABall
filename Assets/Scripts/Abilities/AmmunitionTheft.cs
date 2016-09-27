using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class AmmunitionTheft : MonoBehaviour {

	[Range(0, 1)]
	public float probability = 0.1f;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Ball") {
			int ballsCount = Ball.balls.Count;
			if (ballsCount > 1) {
				if (Random.value < probability) {
					GameObject stolenBall = Ball.balls [Random.Range (0, ballsCount)];
					stolenBall.GetComponent<Ball>().Demolish();
				}
			}
		}
	}
}
