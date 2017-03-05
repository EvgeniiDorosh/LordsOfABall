using UnityEngine;
using System.Collections;

public class PassiveDamageable : MonoBehaviour, IDamageable {

	private BasicStatsController statsController;

	public bool isStable = true;
	[Range(1, 200)]
	public int level = 1;

	private AudioSource audioSource;
	public AudioClip hitSound;

	public Death death;

	private string onGetDamageEvent;

	private float damagePerHit = 1f;

	void Awake() 
	{
		statsController = GetComponent<BasicStatsController> ();
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

	public void ApplyDamage (IAttacker attacker) 
	{
		float damage = isStable ? damagePerHit : attacker.GetDamage(this);
		ApplyDamage (damage);
	}

	public void ApplyDamage (float damage) 
	{
		PlaySingle (hitSound);
		ChangeStatValue(StatType.CurrentHealth, -damage);
	}

	private void OnHealthChanged(float diffValue) {
		if (CurrentHealth <= 0) {
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

	public float CurrentHealth 
	{
		get { return statsController.Get<BaseStat>(StatType.CurrentHealth).Value; }
	}

	public float InitialHealth 
	{
		get { return statsController.Get<BaseStat> (StatType.Health).Value; }
	}

	void PlaySingle(AudioClip clip) {
		if (audioSource.isPlaying) {
			return;
		}
		audioSource.clip = clip;
		audioSource.Play ();
	}
}
