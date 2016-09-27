using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Incorporeity : MonoBehaviour {

	public string targetTag;

	public GameObject missMessage;
	private SpriteRenderer missSprite;

	private Collider2D unitCollider;
	private bool lastTouchWasMiss;

	void Awake () {
		unitCollider = GetComponent<Collider2D> ();
		InitMessage ();
	}

	void InitMessage() {
		missSprite = missMessage.GetComponent<SpriteRenderer> ();
		HideMessage ();
	}

	void HideMessage() {
		missSprite.enabled = false;
	}

	void Start() {
		unitCollider.isTrigger = Random.value > 0.5f;
		lastTouchWasMiss = !unitCollider.isTrigger;
	}

	void OnCollisionEnter2D(Collision2D other) {
		HideMessage ();
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
			missSprite.enabled = true;
			Invoke("HideMessage", 1.5f);
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
