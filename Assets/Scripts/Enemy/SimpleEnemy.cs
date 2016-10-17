using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleEnemy : MonoBehaviour {

	public static readonly List<GameObject> enemies = new List<GameObject>();

	void Awake() {
		enemies.Add (this.gameObject);
	}

	void OnDestroy() {
		enemies.Remove (this.gameObject);
	}
}
