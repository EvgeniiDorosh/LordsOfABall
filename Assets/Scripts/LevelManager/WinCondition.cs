using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WinCondition : MonoBehaviour 
{
	[SerializeField]
	List<GameObject> objectsThatMustBeDestrusted;

	void Awake() 
	{
		if (objectsThatMustBeDestrusted.Count == 0) 
		{
			Debug.LogError ("There are no objects that should be destroyed!");
		}
	}

	void Start()
	{
		IDestructible destructible;
		foreach (GameObject target in objectsThatMustBeDestrusted) 
		{
			destructible = target.GetComponent<IDestructible> ();

			if (destructible != null)
				destructible.Destructed += OnTargetDestructed;
		}
	}

	void OnTargetDestructed(GameObject destructedObject)
	{
		objectsThatMustBeDestrusted.Remove (destructedObject);
		if (objectsThatMustBeDestrusted.Count == 0) 
		{
			Messenger.Invoke (LevelEvent.allEnemiesAreDestroyed);
		}
	}
}
