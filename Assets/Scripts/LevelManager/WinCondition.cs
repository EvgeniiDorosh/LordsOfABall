using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinCondition : MonoBehaviour {

	public List<GameObject> listThatMustBeDestroyed;

	void OnEnable() {
		AddListeners ();
	}

	void OnDisable() {
		RemoveListeners ();
	}

	void AddListeners() {
		Messenger<GameObject>.AddListener (EnemyEvent.enemyWasCreated, ObjectWasCreated);
		Messenger<GameObject>.AddListener (EnemyEvent.enemyWasDestroyed, ObjectWasDestroyed);
		Messenger<GameObject>.AddListener (EnemyEvent.destructibleWasDestroyed, ObjectWasDestroyed);
	}

	void RemoveListeners() {
		Messenger<GameObject>.RemoveListener (EnemyEvent.enemyWasCreated, ObjectWasCreated);
		Messenger<GameObject>.RemoveListener (EnemyEvent.enemyWasDestroyed, ObjectWasDestroyed);
		Messenger<GameObject>.RemoveListener (EnemyEvent.destructibleWasDestroyed, ObjectWasDestroyed);
	}

	void ObjectWasCreated(GameObject target) {
		listThatMustBeDestroyed.Add (target);
	}

	void ObjectWasDestroyed(GameObject target) {
		listThatMustBeDestroyed.Remove (target);
		if (listThatMustBeDestroyed.Count == 0) {
			Messenger.Invoke (LevelEvent.allEnemiesAreDestroyed);
		}
	}
}
