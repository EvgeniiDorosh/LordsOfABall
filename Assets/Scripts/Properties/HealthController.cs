using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

	private Damageable creature;

	public SpriteRenderer healthBar;
	private Vector3 healthBarScale;

	private AudioSource audioSource;
	private DamageableDeath death;
	public AudioClip hitSound;

	void Awake () {
		creature = GetComponent<Damageable> ();

		if (healthBar) {
			healthBarScale = healthBar.transform.localScale;
			healthBar.color = Color.green;
		}

		audioSource = GetComponent<AudioSource> ();
		death = GetComponent<DamageableDeath> ();
	}

	void OnCollisionEnter2D(Collision2D other) {
		Attacker attacker = other.gameObject.GetComponent<Attacker> ();
		if (attacker) {
			creature.ApplyDamage (attacker);
			UpdateHealthBar ();
			PlaySingle (hitSound);
			if (CurrentHealth <= 0) {
				if (death) {
					death.ShowDeath ();
				}
				Destroy (gameObject);
			}
		}
	}

	public void ChangeCurrentHealth(float diff) {
		creature.ParamsController.ChangeParameter("Health", diff);
		UpdateHealthBar ();
	}

	public void UpdateHealthBar() {
		if (healthBar) {
			healthBar.color = Color.Lerp (Color.green, Color.red, (1 - CurrentHealth / InitialHealth));
			healthBar.transform.localScale = new Vector3 (healthBarScale.x, healthBarScale.y * CurrentHealth / InitialHealth, healthBarScale.z);
		}
	}

	public bool HasWounds {
		get { return CurrentHealth < InitialHealth; }
	}

	public float CurrentHealth {
		get { return creature.CurrentParameters.Health; }
	}

	public float InitialHealth {
		get { return creature.InitialParameters.Health; }
	}

	void PlaySingle(AudioClip clip) {
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
