using UnityEngine;
using System;

public class HealthBar : MonoBehaviour 
{
	[SerializeField]
	SpriteRenderer healthBar;
	Vector3 healthBarScale;

	BasicStatsController statsController;

	void Awake () 
	{
		healthBarScale = healthBar.transform.localScale;
		healthBar.color = Color.green;

		statsController = GetComponent<BasicStatsController> ();
	}

	void Start() 
	{
		if (statsController != null) 
		{
			statsController.Get(StatType.CurrentHealth).ValueChanged += OnHealthChanged;
			statsController.Get(StatType.Health).ValueChanged += OnHealthChanged;
			UpdateHealthBar ();
		}
	}

	void OnDisable()
	{
		if (statsController != null) 
		{
			statsController.Get (StatType.CurrentHealth).ValueChanged -= OnHealthChanged;
			statsController.Get (StatType.Health).ValueChanged -= OnHealthChanged;
		}
	}

	void OnHealthChanged(BaseStat stat)
	{
		UpdateHealthBar ();
	}
	
	void UpdateHealthBar() 
	{
		float correlation = statsController.Get (StatType.CurrentHealth).Value / statsController.Get (StatType.Health).Value;
		healthBar.color = Color.Lerp (Color.green, Color.red, (1 - correlation));
		healthBar.transform.localScale = new Vector3 (healthBarScale.x, healthBarScale.y * correlation, healthBarScale.z);
	}
}
