using UnityEngine;
using System.Collections;

public class BallAttacker : Attacker 
{
	string playerTag = "Player";

	override protected void OnCollisionEnter2D(Collision2D other) 
	{
		if (other.gameObject.CompareTag (playerTag))
			return;
		base.OnCollisionEnter2D (other);
	}
}
