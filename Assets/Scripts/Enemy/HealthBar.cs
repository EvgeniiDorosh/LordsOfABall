using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public SpriteRenderer healthBar;
	private Vector3 healthBarScale;

	void Awake () {
		healthBarScale = healthBar.transform.localScale;
		healthBar.color = Color.green;
	}

	void OnEnable() {
		UpdateHealthBar (1f);
	}
	
	public void UpdateHealthBar(float correlation) {
		healthBar.color = Color.Lerp (Color.green, Color.red, (1 - correlation));
		healthBar.transform.localScale = new Vector3 (healthBarScale.x, healthBarScale.y * correlation, healthBarScale.z);
	}
}
