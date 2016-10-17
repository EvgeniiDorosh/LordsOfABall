using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CreatureParametersController))]
public class Damageable : MonoBehaviour, IDamageable {

	private CreatureParametersController paramsController;

	private HealthBar healthBar;
	private DamageableDeath death;
	private AudioSource audioSource;
	public AudioClip hitSound;

	void Awake() {
		paramsController = GetComponent<CreatureParametersController> ();

		healthBar = GetComponent<HealthBar> ();
		audioSource = GetComponent<AudioSource> ();
		death = GetComponent<DamageableDeath> ();
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
			if (death) {
				death.ShowDeath ();
			}
			Destroy (gameObject);
		}
	}

	void OnDestroy() {
		Messenger<CreatureVO>.Invoke (CreatureEvent.creatureWasDestroyed, gameObject.GetCreatureVO());
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
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
