﻿using UnityEngine;
using System.Collections;

public class Abyss : MonoBehaviour {

	private bool isReflexive;
	private BoxCollider2D boxCollider;

	public bool IsReflexive {
		get { 
			return isReflexive;
		}
		set { 
			isReflexive = value;
			boxCollider.isTrigger = !value;
		}
	}

	void Awake() {
		boxCollider = GetComponent<BoxCollider2D> ();
		IsReflexive = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		GameObject otherGameObject = other.gameObject;
		if (otherGameObject.CompareTag ("Ball")) {
			if (Ball.balls.Count == 1) {
				// Inherit of Attacker, damage paddle with inverse proportion of enemies amount damage;
			}
			otherGameObject.GetComponent<Ball>().Demolish();
		}
	}
}