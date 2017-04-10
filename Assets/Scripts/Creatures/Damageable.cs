using UnityEngine;
using System.Collections;
using System;

public class Damageable : MonoBehaviour, IDamageable 
{
	BasicStatsController statsController;

	AudioSource audioSource;
	[SerializeField]
	AudioClip hitSound;

	public event EventHandler GotDamage;
	public event DestructDelegate Destructed;

	void Awake() 
	{
		statsController = GetComponent<BasicStatsController> ();
		audioSource = GetComponent<AudioSource> ();
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
			Destruct ();
		else if (diffValue < 0)
			if (GotDamage != null)
				GotDamage (this, null);
	}

	public void Destruct () 
	{
		gameObject.SetActive(false);
		if (Destructed != null)
			Destructed (gameObject);
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
