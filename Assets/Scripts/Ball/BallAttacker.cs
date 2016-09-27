using UnityEngine;
using System.Collections;

public class BallAttacker : Attacker {

	override protected void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			return;
		}

		base.OnCollisionEnter2D (other);
	}
}
