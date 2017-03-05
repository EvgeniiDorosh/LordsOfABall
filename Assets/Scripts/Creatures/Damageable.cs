using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour, IDamageable 
{
	private BasicStatsController statsController;

	private HealthBar healthBar;

	private AudioSource audioSource;
	public AudioClip hitSound;

	public Death death;

	private string onGetDamageEvent;

	void Awake() 
	{
		statsController = GetComponent<BasicStatsController> ();

		healthBar = GetComponent<HealthBar> ();
		audioSource = GetComponent<AudioSource> ();

		onGetDamageEvent = CreatureEvent.creatureGotDamage + gameObject.GetInstanceID ();
	}

	public float GetStatValue(StatType type)
	{
		return statsController.Get<BaseStat> (type).Value;
	}

	public void ChangeStatValue (StatType type, float diffValue) 
	{
		statsController.ChangeValue<BaseStat>(type, diffValue);
		switch (type) {
		case StatType.CurrentHealth:
			OnHealthChanged (diffValue);
			break;
		}
	}

	public virtual void ApplyDamage (IAttacker attacker) 
	{		
		float damage = attacker.GetDamage(this);
		ApplyDamage (damage);
	}

	public virtual void ApplyDamage(float damage) 
	{
		PlaySingle (hitSound);
		ChangeStatValue(StatType.CurrentHealth, -damage);
	}

	private void OnHealthChanged(float diffValue) 
	{
		if (CurrentHealth <= 0) 
		{
			Demolish ();
		}
		else if (diffValue < 0) 
		{
			Messenger<float>.Invoke (onGetDamageEvent, diffValue);
		}

		if (healthBar) 
		{
			healthBar.UpdateHealthBar (CurrentHealth / InitialHealth);
		}
	}

	public void Demolish () 
	{
		if (death != null) 
		{
			death.ShowDeath ();
		}
		gameObject.SetActive(false);
		Messenger<GameObject>.Invoke (CreatureEvent.creatureWasDestroyed, this.gameObject);
	}

	public bool HasWounds 
	{
		get { return CurrentHealth < InitialHealth; }
	}

	public float CurrentHealth 
	{
		get { return statsController.Get<BaseStat>(StatType.CurrentHealth).Value; }
	}

	public float InitialHealth 
	{
		get { return statsController.Get<BaseStat> (StatType.Health).Value; }
	}

	void PlaySingle(AudioClip clip) 
	{
		if (audioSource.isPlaying)
			return;
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
