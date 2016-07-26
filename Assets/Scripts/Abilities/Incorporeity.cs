using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Incorporeity : MonoBehaviour {

	public string targetTag;
	public GameObject missMessage;

	private Collider2D unitCollider;
	private bool lastTouchWasMiss;

	void Awake () {
		unitCollider = GetComponent<Collider2D> ();
	}

	void Start() {
		unitCollider.isTrigger = Random.value > 0.5f;
		lastTouchWasMiss = !unitCollider.isTrigger;
	}

	void OnCollisionExit2D(Collision2D other) {
		if (other.gameObject.tag == targetTag) {
			if (lastTouchWasMiss) {
				lastTouchWasMiss = false;
				unitCollider.isTrigger = Random.value < 0.5f;
			} else {
				unitCollider.isTrigger = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == targetTag) {
			GameObject message = Instantiate(missMessage, transform.position, Quaternion.identity) as GameObject;
			Destroy (message, 1.5f);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == targetTag) {
			if (lastTouchWasMiss) {
				unitCollider.isTrigger = false;
			} else {
				lastTouchWasMiss = true;
				unitCollider.isTrigger = Random.value < 0.5f;
			}
		}
	}
}
