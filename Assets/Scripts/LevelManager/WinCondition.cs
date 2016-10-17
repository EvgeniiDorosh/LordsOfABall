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
		Messenger<CreatureVO>.AddListener (CreatureEvent.creatureWasDestroyed, ObjectWasDestroyed);
	}

	void RemoveListeners() {
		Messenger<GameObject>.AddListener (CreatureEvent.creatureWasCreated, ObjectWasCreated);
		Messenger<CreatureVO>.AddListener (CreatureEvent.creatureWasDestroyed, ObjectWasDestroyed);
	}

	void ObjectWasCreated(GameObject createdObject) {
		objectsThatMustBeDestroyed.Add (createdObject);
	}

	void ObjectWasDestroyed(CreatureVO destroyedObject) {
		foreach(GameObject target in objectsThatMustBeDestroyed) {
			if (target.GetInstanceID () == destroyedObject.instanceID) {
				objectsThatMustBeDestroyed.Remove (target);
			}
		}

		if (objectsThatMustBeDestroyed.Count == 0) {
			Messenger.Invoke (LevelEvent.allEnemiesAreDestroyed);
		}
	}
}
