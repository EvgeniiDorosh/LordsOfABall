using UnityEngine;
using System.Collections;

public class SimplePortal : MonoBehaviour {

	public GameObject enter;
	public Transform exit;
	private bool isOpened = true;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Ball") {
			if (isOpened) {
				other.gameObject.transform.position = exit.position;
				Close ();
				Invoke ("Open", 5f);
			}
		}
	}

	void Close() {
		isOpened = false;
		enter.GetComponent<Animator> ().Stop ();
		enter.transform.localScale = new Vector2 (0.2f, 0.2f);
	}

	void Open() {
		isOpened = true;
		enter.GetComponent<Animator> ().Play ("Portal");
		enter.transform.localScale = new Vector2 (1f, 1f);
	}
}
