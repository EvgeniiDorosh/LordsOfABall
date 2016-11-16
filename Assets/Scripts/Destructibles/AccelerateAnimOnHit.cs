using UnityEngine;
using System.Collections;

public class AccelerateAnimOnHit : MonoBehaviour {

	private Animator animator;

	void Awake () {
		animator = GetComponent<Animator> ();
	}
	
	void Start () {
		animator.speed = 0;
	}

	void OnCollisionEnter2D(Collision2D other) {
		animator.speed += 1;
	}

}
