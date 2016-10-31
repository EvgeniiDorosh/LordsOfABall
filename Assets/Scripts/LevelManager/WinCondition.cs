using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinCondition : MonoBehaviour {

	public List<GameObject> objectsThatMustBeDestroyed;

	void Awake() {
		if (objectsThatMustBeDestroyed.Count == 0) {
			Debug.LogError ("There are no objects that should be destroyed!");
		}
	}

	void OnEnable() {
		AddListeners ();
	}

	void OnDisable() {
		RemoveListeners ();
	}

	void AddListeners() {
		Messenger<GameObject>.AddListener (CreatureEvent.creatureWasCreated, ObjectWasCreated);
		Messenger<GameObject>.AddListener (CreatureEvent.creatureWasDestroyed, ObjectWasDestroyed);
	}

	void RemoveListeners() {
		Messenger<GameObject>.RemoveListener (CreatureEvent.creatureWasCreated, ObjectWasCreated);
		Messenger<GameObject>.RemoveListener (CreatureEvent.creatureWasDestroyed, ObjectWasDestroyed);
	}

	void ObjectWasCreated(GameObject createdObject) {
		objectsThatMustBeDestroyed.Add (createdObject);
	}

	void ObjectWasDestroyed(GameObject destroyedObject) {
		objectsThatMustBeDestroyed.Remove (destroyedObject);
		if (objectsThatMustBeDestroyed.Count == 0) {
			Messenger.Invoke (LevelEvent.allEnemiesAreDestroyed);
		}
	}
}
