using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleEnemy : Damageable {

	public static readonly List<GameObject> enemies = new List<GameObject>();

	public string creatureName;

	new void Awake() {
		base.Awake ();
		enemies.Add (this.gameObject);
		InitialParameters = ConfigsParser.instance.enemiesConfig.GetParametersByName (creatureName);
		CurrentParameters = InitialParameters.Clone();
	}

	void OnCollisionEnter2D(Collision2D other) {
		
	}

	void OnDestroy() {
		enemies.Remove (this.gameObject);
	}
}
