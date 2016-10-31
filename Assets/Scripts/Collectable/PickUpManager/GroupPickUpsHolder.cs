using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GroupPickUpsHolder : MonoBehaviour, IPickUpsHolder {

	[Serializable]
	public class GroupPickUpTask {
		public List<GameObject> targetObjects;
		public List<GameObject> pickUps;

		public Transform spawnPoint;
	}

	public List<GroupPickUpTask> taskList = new List<GroupPickUpTask> (); 

	public bool InstantiatePickUpFor (GameObject targetObject) {
		foreach (GroupPickUpTask task in taskList) {
			List<GameObject> currentList = task.targetObjects;
			currentList.Remove (targetObject);
			if (currentList.Count == 0) {
				GameObject pickUp = Instantiate(task.pickUps [Random.Range (0, task.pickUps.Count)]);
				if (task.spawnPoint != null) {
					pickUp.transform.position = task.spawnPoint.position;
				} else {
					pickUp.transform.position = targetObject.transform.position;
				}
				pickUp.SetActive (true);
				taskList.Remove (task);
				return true;
			}
		}

		return false;
	}
}
