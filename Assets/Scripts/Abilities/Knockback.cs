using UnityEngine;
using System.Collections;

public class Knockback : MonoBehaviour 
{
	[Range(0, 1)]
	public float probability;

	[SerializeField]
	AudioSource audioSource;
	string targetTag = "Ball";

	void OnCollisionEnter2D(Collision2D other) 
	{
		GameObject otherObject = other.gameObject;
		if (otherObject.CompareTag (targetTag)) 
		{
			if (otherObject.GetComponent<BallImpulse> () != null) 
			{
				return;
			}

			if (Random.value < probability)
			{
				audioSource.Play ();
				other.gameObject.AddComponent<BallImpulse> ();
			}
 		}
	}
}
