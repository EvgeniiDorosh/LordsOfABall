using UnityEngine;
using System.Collections;

public class Abyss : MonoBehaviour {

	private bool isReflexive;

	public bool IsReflexive {
		get { 
			return isReflexive;
		}
		set { 
			isReflexive = value;
			GetComponent<BoxCollider2D> ().isTrigger = !value;
		}
	}

	void Awake() {
		IsReflexive = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.CompareTag ("Ball")) {
			Destroy (other.gameObject);
		}
	}
}
