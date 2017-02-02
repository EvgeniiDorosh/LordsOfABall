using System.Collections.Generic;
using UnityEngine;

public class Pool : Object
{
	List<GameObject> predefinedObjects;
	Transform parent;
	GameObject target;

	public Pool (PoolVO item, Transform parent)
	{
		this.parent = parent;
		this.target = item.target;
		predefinedObjects = CreatePool (item, parent);
	}

	List<GameObject> CreatePool(PoolVO item, Transform parent)
	{
		lock (this) 
		{
			List<GameObject> result = new List<GameObject> ();
			for (int i = 0; i < item.size; i++) 
			{
				GameObject cloneObject = Instantiate(item.target, parent) as GameObject;
				cloneObject.SetActive (false);
				result.Add(cloneObject);
			}
			return result;
		}
	}

	public GameObject SpawnObject(Vector2 position) 
	{
		GameObject spawnedObject = GetPooledObject();
		if (spawnedObject != null) 
		{
			spawnedObject.SetActive (true);
			spawnedObject.transform.position = position;
		}

		return spawnedObject;
	}

	GameObject GetPooledObject() 
	{
		lock (this) 
		{
			foreach (GameObject item in predefinedObjects) 
			{
				if (!item.activeInHierarchy) 
				{
					return item;
				}
			}

			GameObject cloneObject = Instantiate(target, parent) as GameObject;
			predefinedObjects.Add(cloneObject);
			return cloneObject;
		}
	}

	public void Clear()
	{
		predefinedObjects.Clear ();
	}
}

