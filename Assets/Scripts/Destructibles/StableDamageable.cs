using UnityEngine;
using System.Collections;

public class StableDamageable : MonoBehaviour, IDamageable {

	[Range(1, 200)]
	public int level = 1;

	private float initialHealth;
	private float currentHealth;

	private DamageableDeath death;
	private AudioSource audioSource;
	public AudioClip hitSound;

	private float damagePerHit = 1f;

	public void ApplyDamage (IAttacker attacker) {
		ApplyDamage (damagePerHit);
	}

	public void ApplyDamage (float damage) {
		ChangeParameter ("Health", -damage);
		OnGetDamage ();
	}

	public float GetCurrentValue (string paramName) {
		if (paramName == "Health") {
			return currentHealth;
		}
		return 0f;
	}

	public float GetInitialValue (string paramName) {
		if (paramName == "Health") {
			return initialHealth;
		}
		return 0f;
	}

	public void ChangeParameter (string paramName, float diffValue) {
		switch (paramName) {
		case "Health" :
			currentHealth += diffValue;
			break;
		case "InitialHealth":
			initialHealth += diffValue;
			break;
		}
		CheckDeath ();
	}

	void Awake() {
		initialHealth = (float)level;
		currentHealth = initialHealth;

		audioSource = GetComponent<AudioSource> ();
		death = GetComponent<DamageableDeath> ();
	}

	private void OnGetDamage() {
		PlaySingle (hitSound);
		CheckDeath ();
	}

	void CheckDeath() {
		if (currentHealth <= 0) {
			Demolish ();
		}
	}

	public void Demolish () {
		if (death) {
			death.ShowDeath ();
		}
		Messenger<GameObject>.Invoke (CreatureEvent.creatureWasDestroyed, this.gameObject);
		Destroy (gameObject);
	}

	void PlaySingle(AudioClip clip) {
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
