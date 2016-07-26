using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleEnemy : MonoBehaviour, Damageable {

	public static readonly List<SimpleEnemy> enemies = new List<SimpleEnemy>();

	public string creatureName;
	private BaseCreatureParameters initialParameters;
	public BaseCreatureParameters currentParameters;

	void Awake() {
		enemies.Add (this);
	}

	void Start () {
		initialParameters = ConfigsParser.instance.enemiesConfig.GetParametersByName (creatureName);
		currentParameters = initialParameters.Clone ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D other) {
		
	}

	void OnDestroy() {
		enemies.Remove (this);
	}

	public void ApplyDamage(float damage) {
		
	}
}
