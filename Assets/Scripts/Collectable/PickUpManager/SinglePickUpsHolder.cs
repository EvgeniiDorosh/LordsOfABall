using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SinglePickUpsHolder : MonoBehaviour, IPickUpsHolder {

	[Serializable]
	public class SinglePickUpTask {
		
		public List<GameObject> targetObjects;
		public List<GameObject> pickUps;

		[Range(0, 1)]
		public float initialProbability;

		public bool useDynamicBalance;
		[Range(0, 1)]
		public float unitProbability;
	}

	public List<SinglePickUpTask> taskList = new List<SinglePickUpTask>();
	Dictionary<GameObject, GameObject> pickUps = new Dictionary<GameObject, GameObject> ();

	public bool InstantiatePickUpFor (GameObject targetObject) {
		if (pickUps.ContainsKey (targetObject)) {
			GameObject pickUp = Instantiate(pickUps [targetObject]);
			pickUp.transform.position = targetObject.transform.position;
			pickUp.SetActive (true);
			return true;
		}

		return false;
	}

	void Awake () {
		foreach (SinglePickUpTask task in taskList) {
			float probability = task.initialProbability;
			foreach(GameObject targetObject in task.targetObjects) {
				Debug.Log ("Probability = " + probability);
				if (pickUps.ContainsKey (targetObject)) {
					continue;
				}

				if (Random.value < probability) {
					GameObject pickUp = task.pickUps [Random.Range (0, task.pickUps.Count)];
					pickUps [targetObject] = pickUp;
					if (task.useDynamicBalance) {
						probability = Mathf.Clamp (probability - task.unitProbability, 0, 1);
					}
					continue;
				}

				if (task.useDynamicBalance) {
					probability = Mathf.Clamp (probability + task.unitProbability, 0, 1);
				}
			}		
		}
	}
}
