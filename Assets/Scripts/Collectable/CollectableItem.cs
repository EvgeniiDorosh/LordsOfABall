using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class CollectableItem : MonoBehaviour {

	public string itemName;
	public bool isOneTimeUse;
	public Vector2 speed;
	public List<string> targetTags;

	public abstract void Trigger (GameObject targetObject);

	void Update() {
		transform.Translate (speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		string otherTag = other.gameObject.tag;
		if (targetTags.Contains (otherTag)) {
			Trigger (other.gameObject);
			if (isOneTimeUse) {
				GetComponent<DamageableDeath> ().ShowDeath ();
				Destroy(gameObject);
			}
		}
	}
}
