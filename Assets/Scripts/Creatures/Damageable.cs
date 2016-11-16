using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour, IDamageable {

	private CreatureParametersController paramsController;

	private HealthBar healthBar;
	private AudioSource audioSource;
	public AudioClip hitSound;
	public Death death;

	void Awake() {
		paramsController = GetComponent<CreatureParametersController> ();

		healthBar = GetComponent<HealthBar> ();
		audioSource = GetComponent<AudioSource> ();
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
			OnHealthChanged ();
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

	private void OnHealthChanged() {
		if (healthBar) {
			healthBar.UpdateHealthBar (CurrentHealth / InitialHealth);
		}
		if (CurrentHealth <= 0) {
			Demolish ();
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
