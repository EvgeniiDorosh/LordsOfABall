using UnityEngine;
using System.Collections;

public class SpriteChanger : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private Sprite defaultSprite;
	public Sprite[] sprites;

	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		defaultSprite = spriteRenderer.sprite;
	}

	/*void OnEnable() {
		spriteRenderer.sprite = defaultSprite;
		string destinationEvent = CreatureEvent.creatureParameterWasUpdated + gameObject.GetInstanceID ();
		Messenger<StatChange>.AddListener (destinationEvent, OnChangeHealth);
	}

	void OnDisable() {
		string destinationEvent = CreatureEvent.creatureParameterWasUpdated + gameObject.GetInstanceID ();
		Messenger<StatChange>.RemoveListener (destinationEvent, OnChangeHealth);
	}

	void OnChangeHealth(StatChange statChange) {
		if (statChange.name == "Health") {
			float percentage = (1f - Mathf.Clamp01 (statChange.current / statChange.initial));
			int index = Mathf.CeilToInt(percentage * (sprites.Length - 1));
			spriteRenderer.sprite = sprites[index];
		}
	}*/
}
