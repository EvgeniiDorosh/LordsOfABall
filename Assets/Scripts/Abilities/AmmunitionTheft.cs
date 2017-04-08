using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class AmmunitionTheft : MonoBehaviour 
{
	[Range(0, 1)]
	public float probability = 0.1f;

	string targetTag = "Ball";

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag(targetTag)) 
		{
			int ballsCount = MembersAccount.Count(Member.Ball);
			if (ballsCount > 1) 
			{
				if (Random.value < probability) 
				{
					GameObject stolenBall = MembersAccount.Get (Member.Ball) [Random.Range (0, ballsCount)];
					stolenBall.GetComponent<Ball>().Destruct();
				}
			}
		}
	}
}
