using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class SinglePickUpsHolder : MonoBehaviour 
{
	[SerializeField]
	Task[] taskList;

	Dictionary<GameObject, GameObject> pickUpsDistribution = new Dictionary<GameObject, GameObject> ();

	void Awake () 
	{
		IDestructible destructible;
		foreach (Task task in taskList) 
		{
			float probability = task.initialProbability;
			foreach(GameObject targetObject in task.targetObjects) 
			{
				if (pickUpsDistribution.ContainsKey (targetObject))
					continue;

				destructible = targetObject.GetComponent<IDestructible> ();
				if(destructible == null)
					continue;

				if (Random.value < probability) 
				{
					GameObject pickUp = task.pickUps [Random.Range (0, task.pickUps.Length)];
					destructible.Destructed += OnTargetDestructed;
					pickUpsDistribution [targetObject] = pickUp;

					if (task.useDynamicBalance)
						probability = Mathf.Clamp (probability - task.unitProbability, 0, 1f);
					continue;
				}

				if (task.useDynamicBalance)
					probability = Mathf.Clamp (probability + task.unitProbability, 0, 1f);
			}		
		}
	}

	void OnTargetDestructed(GameObject targetObject)
	{
		if (pickUpsDistribution.ContainsKey (targetObject)) 
		{
			GameObject pickUp = Instantiate(pickUpsDistribution [targetObject]);
			pickUp.transform.position = targetObject.transform.position;
			pickUp.SetActive (true);
		}
	}

	[Serializable]
	public class Task
	{
		public GameObject[] targetObjects;
		public GameObject[] pickUps;

		[Range(0, 1)]
		public float initialProbability;

		public bool useDynamicBalance;
		[Range(0, 1)]
		public float unitProbability;
	}
}
