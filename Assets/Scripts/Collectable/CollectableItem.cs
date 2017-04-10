using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class CollectableItem : MonoBehaviour 
{
	public string itemName;
	public Vector2 speed = Vector2.down;
	public List<string> targetTags;

	public GameObject spell;

	void Update() {
		transform.Translate (speed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other) {
		string otherTag = other.gameObject.tag;
		if (targetTags.Contains (otherTag)) {
			AddSpell (other.gameObject);
			Destroy(gameObject);
		}
	}

	void AddSpell(GameObject targetObject) {
		GameObject cloneSpell = Instantiate (spell) as GameObject;
		cloneSpell.transform.parent = targetObject.transform;
		Spell spellEffect = cloneSpell.GetComponent<Spell> ();
		spellEffect.Trigger(targetObject);
	}
}
