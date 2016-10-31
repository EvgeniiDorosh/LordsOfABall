using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpsManager : MonoBehaviour {

	IPickUpsHolder[] pickUpsHolders;

	void Awake () {
		pickUpsHolders = GetComponents<IPickUpsHolder> ();
	}

	void OnEnable() {
		Messenger<GameObject>.AddListener (CreatureEvent.creatureWasDestroyed, CheckForPickUp);
	}

	void OnDisable() {
		Messenger<GameObject>.RemoveListener (CreatureEvent.creatureWasDestroyed, CheckForPickUp);
	}

	void CheckForPickUp(GameObject target) {
		foreach (IPickUpsHolder holder in pickUpsHolders) {
			holder.InstantiatePickUpFor (target);
		}
	}
}
