using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GroupPickUpsHolder : MonoBehaviour 
{
	[SerializeField]
	List<Task> taskList;

	List<Task> pickUpsDistribution = new List<Task>();

	void Awake()
	{
		IDestructible destructible;
		List<GameObject> targets = new List<GameObject> ();
		foreach (Task task in taskList) 
		{
			pickUpsDistribution.Add (task);
			foreach (GameObject targetObject in task.targetObjects) 
			{
				if (targets.Contains (targetObject))
					continue;

				targets.Add (targetObject);
				destructible = targetObject.GetComponent<IDestructible> ();

				if (destructible != null)
					destructible.Destructed += OnTargetDestructed;
			}
		}
	}

	void OnTargetDestructed(GameObject targetObject)
	{
		foreach (Task task in pickUpsDistribution) 
		{
			List<GameObject> currentList = task.targetObjects;
			currentList.Remove (targetObject);
			if (currentList.Count == 0) 
			{
				GameObject pickUp = Instantiate(task.pickUps [Random.Range (0, task.pickUps.Length)]);
				pickUp.transform.position = (task.spawnPoint != null) ? task.spawnPoint.position : targetObject.transform.position;
				pickUp.SetActive (true);
				pickUpsDistribution.Remove (task);
			}
		}
	}

	[Serializable]
	public class Task 
	{
		public List<GameObject> targetObjects;
		public GameObject[] pickUps;

		public Transform spawnPoint;
	}
}
