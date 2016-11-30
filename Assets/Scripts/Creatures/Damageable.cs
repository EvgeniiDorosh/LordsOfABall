using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour, IDamageable {

	private CreatureParametersController paramsController;

	private HealthBar healthBar;

	private AudioSource audioSource;
	public AudioClip hitSound;

	public Death death;

	private string onGetDamageEvent;

	void Awake() {
		paramsController = GetComponent<CreatureParametersController> ();

		healthBar = GetComponent<HealthBar> ();
		audioSource = GetComponent<AudioSource> ();

		onGetDamageEvent = CreatureEvent.creatureGotDamage + gameObject.GetInstanceID ();
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

	public virtual void ApplyDamage (IAttacker attacker) {		
		float damage = attacker.GetDamage(this);
		ApplyDamage (damage);
	}

	public virtual void ApplyDamage(float damage) {
		PlaySingle (hitSound);
		ChangeParameter("Health", -damage);
	}

	private void OnHealthChanged(float diffValue) {
		if (CurrentHealth <= 0) {
			Demolish ();
		} else if (diffValue < 0) {
			Messenger<float>.Invoke (onGetDamageEvent, diffValue);
		}

		if (healthBar) {
			healthBar.UpdateHealthBar (CurrentHealth / InitialHealth);
		}
	}

	public void Demolish () {
		if (death != null) {
			death.ShowDeath ();
		}
		gameObject.SetActive(false);
		Messenger<GameObject>.Invoke (CreatureEvent.creatureWasDestroyed, this.gameObject);
	}

	public bool HasWounds {
		get { return CurrentHealth < InitialHealth; }
	}

	public float CurrentHealth {
		get { return GetCurrentValue("Health"); }
	}

	public float InitialHealth {
		get { return GetInitialValue("Health"); }
	}

	void PlaySingle(AudioClip clip) {
		if (audioSource.isPlaying) {
			return;
		}
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
