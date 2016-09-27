using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destructible : MonoBehaviour {

	public static readonly List<GameObject> destructibles = new List<GameObject>();

	void Awake() {
		destructibles.Add (this.gameObject);
	}

	void OnDestroy() {
		destructibles.Remove (this.gameObject);
		Messenger<GameObject>.Invoke (EnemyEvent.destructibleWasDestroyed, this.gameObject);
	}
}
