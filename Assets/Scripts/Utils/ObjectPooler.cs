using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour 
{
	static ObjectPooler instance = null;

	static List<GameObject> poolsParents = new List<GameObject>();
	static Dictionary<int, Pool> objectPoolers = new Dictionary<int, Pool>();

	void Awake () 
	{
		if (instance != null) 
		{
			Destroy (gameObject);
			return;
		}
		instance = this;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	void OnSceneUnloaded(Scene scene)
	{
		foreach (KeyValuePair<int, Pool> pool in objectPoolers)
			pool.Value.Clear ();
		foreach (GameObject parent in poolsParents)
			Destroy (parent);
		poolsParents.Clear ();
		objectPoolers.Clear ();
	}

	public static Pool CreatePool(PoolVO item)
	{
		int key = item.type;
		if (objectPoolers.ContainsKey (key)) 
			return objectPoolers [key];
		GameObject parent = Instantiate (new GameObject (item.target.name), instance.transform) as GameObject;
		poolsParents.Add (parent);
		Pool pool = new Pool (item, parent.transform);
		objectPoolers.Add(key, pool);
		return pool;
	}

	public static Pool GetPool(int key)
	{
		if (objectPoolers.ContainsKey (key)) 
			return objectPoolers [key];
		return null;
	}
}