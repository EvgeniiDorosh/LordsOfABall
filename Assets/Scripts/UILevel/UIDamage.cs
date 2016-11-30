using UnityEngine;
using System.Collections;

public class UIDamage : MonoBehaviour {

	private Animator anim;

	string paddleGotDamageEvent;

	void Awake () {
		anim = GetComponent<Animator>();
	}
	
	void OnEnable() {
		int paddleId = GameObject.FindGameObjectWithTag ("Player").GetInstanceID ();
		paddleGotDamageEvent = CreatureEvent.creatureGotDamage + paddleId;
		Messenger<float>.AddListener (paddleGotDamageEvent, ShowDamageEffect);
	}

	void OnDisable() {
		Messenger<float>.RemoveListener (paddleGotDamageEvent, ShowDamageEffect);
	}

	void ShowDamageEffect(float damage) {
		anim.SetTrigger ("GotDamage");
	}
}
