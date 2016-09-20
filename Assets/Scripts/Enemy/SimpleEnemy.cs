using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleEnemy : Damageable {

	public static readonly List<GameObject> enemies = new List<GameObject>();

	public string creatureName;

	new void Awake() {
		base.Awake ();
		ParamsController.InitialParameters = ConfigsParser.instance.enemiesConfig.GetParametersByName (creatureName);
		ParamsController.CurrentParameters = InitialParameters.Clone();

		enemies.Add (this.gameObject);
	}

	void OnDestroy() {
		enemies.Remove (this.gameObject);
		Messenger<GameObject>.Invoke(EnemyEvent.enemyWasDestroyed, this.gameObject);
	}
}
