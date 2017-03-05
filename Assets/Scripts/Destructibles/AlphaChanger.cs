using UnityEngine;
using System.Collections;

public class AlphaChanger : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	/*void OnEnable() {
		Color color = spriteRenderer.color;
		color.a = 1f;
		spriteRenderer.color = color;
		string destinationEvent = CreatureEvent.creatureParameterWasUpdated + gameObject.GetInstanceID ();
		Messenger<StatChange>.AddListener (destinationEvent, OnChangeHealth);
	}

	void OnDisable() {
		string destinationEvent = CreatureEvent.creatureParameterWasUpdated + gameObject.GetInstanceID ();
		Messenger<StatChange>.RemoveListener (destinationEvent, OnChangeHealth);
	}

	void OnChangeHealth(StatChange statChange) {
		if (statChange.name == "Health") {
			float alpha = Mathf.Clamp01 (statChange.current / statChange.initial);
			spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
		}
	}*/
}
