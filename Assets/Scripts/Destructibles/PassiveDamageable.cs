using UnityEngine;
using System.Collections;

public class PassiveDamageable : MonoBehaviour, IDamageable {

	private DestructibleParametersController paramsController;

	public bool isStable = true;
	[Range(1, 200)]
	public int level = 1;

	private AudioSource audioSource;
	public AudioClip hitSound;

	public Death death;

	private string onGetDamageEvent;

	private float damagePerHit = 1f;

	void Awake() {
		paramsController = GetComponent<DestructibleParametersController> ();
		audioSource = GetComponent<AudioSource> ();

		onGetDamageEvent = CreatureEvent.creatureGotDamage + gameObject.GetInstanceID ();
	}

	public void ApplyDamage (IAttacker attacker) {
		float damage = isStable ? damagePerHit : attacker.GetDamage(this);
		ApplyDamage (damage);
	}

	public void ApplyDamage (float damage) {
		PlaySingle (hitSound);
		ChangeParameter ("Health", -damage);
	}

	public float GetCurrentValue (string paramName) {
		return paramsController.CurrentParameters.GetValue(paramName);
	}

	public float GetInitialValue (string paramName) {
		return paramsController.InitialParameters.GetValue(paramName);
	}

	public void ChangeParameter (string paramName, float diffValue) {
		paramsController.ChangeParameter (paramName, diffValue);
		switch (paramName) {
		case "Health":
			OnHealthChanged (diffValue);
			break;
		}
	}

	private void OnHealthChanged(float diffValue) {
		if (GetCurrentValue("Health") <= 0) {
			Demolish ();
		} else if (diffValue < 0) {
			Messenger<float>.Invoke (onGetDamageEvent, diffValue);
		}
	}

	public void Demolish () {
		if (death != null) {
			death.ShowDeath ();
		}
		gameObject.SetActive (false);
		Messenger<GameObject>.Invoke (CreatureEvent.creatureWasDestroyed, this.gameObject);
	}

	void PlaySingle(AudioClip clip) {
		if (audioSource.isPlaying) {
			return;
		}
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
