using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleEnemy : Damageable {

	public static readonly List<GameObject> enemies = new List<GameObject>();

	public string creatureName;

	void Awake() {
		enemies.Add (this.gameObject);
	}

	void Start () {
		initialParameters = ConfigsParser.instance.enemiesConfig.GetParametersByName (creatureName);
		currentParameters = initialParameters.Clone<BaseCreatureParameters> ();
	}

	void OnCollisionEnter2D(Collision2D other) {
		
	}

	void OnDestroy() {
		enemies.Remove (this.gameObject);
	}

	public void ApplyDamage(float damage) {
		
	}
}
