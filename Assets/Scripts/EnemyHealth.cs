using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public SpriteRenderer healthBar;
	private Vector3 healthBarScale;

	public float health = 1.0f;
	public float currentHealth { get; set; }

	private AudioSource audioSource;
	private EnemyDeath death;
	public AudioClip hitSound;

	void Awake () {
		resetCurrentHealth ();
		healthBarScale = healthBar.transform.localScale;
		healthBar.color = Color.green;

		audioSource = GetComponent<AudioSource> ();
		death = GetComponent<EnemyDeath> ();
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Ball") {
			changeCurrentHealth(-1.0f); // FIXME : Change by damage;
			PlaySingle (hitSound);
			if (currentHealth <= 0) {
				death.ShowDeath ();
				Destroy (gameObject);
			}
		}
	}

	public void resetCurrentHealth() {
		currentHealth = health;
	}

	public void changeCurrentHealth(float diff) {
		currentHealth += diff;
		if (!hasWounds) {
			resetCurrentHealth ();
		}
		UpdateHealthBar ();
	}

	public void UpdateHealthBar() {
		healthBar.color = Color.Lerp (Color.green, Color.red, (1 - currentHealth / health));
		healthBar.transform.localScale = new Vector3 (healthBarScale.x, healthBarScale.y * currentHealth / health, healthBarScale.z);
	}

	public bool hasWounds {
		get { return currentHealth < health; }
	}

	void PlaySingle(AudioClip clip) {
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
