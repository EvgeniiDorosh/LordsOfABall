using UnityEngine;
using System.Collections;

public class AnimationAccelerator : MonoBehaviour {

	private Animator animator;
	private int maxSpeed = 10;

	void Awake () {
		animator = GetComponent<Animator> ();
		animator.speed = 0;
	}

	/*void OnEnable() {
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
			animator.speed = Mathf.Ceil(percentage * maxSpeed);
		}
	}*/
}
