using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	private static readonly Dictionary<PooledType, ObjectPooler> objectPoolers = new Dictionary<PooledType, ObjectPooler>();

	public int size = 0;
	public bool isGrowing = true;
	public PooledType pooledType;
	public GameObject targetObject;

	[Tooltip("If filled, target object will be ignored")]
	public List<GameObject> predefinedObjects = new List<GameObject>();

	void Awake () {
		if (objectPoolers.ContainsKey(pooledType)) {
			DestroyImmediate (gameObject);
			return;
		}
		if (predefinedObjects == null && targetObject == null) {
			Debug.LogError ("There is nothing to pool in " + pooledType.ToString ());
			DestroyImmediate (gameObject);
			return;
		}
		objectPoolers.Add (pooledType, this);
	}
	
	void Start () {
		if (predefinedObjects.Count == 0) {
			for (int i = 0; i < size; i++) {
				GameObject cloneObject = Instantiate(targetObject, Vector2.zero, Quaternion.identity) as GameObject;
				predefinedObjects.Add(cloneObject);
			}
		}

		foreach (GameObject cloneObject in predefinedObjects) {
			cloneObject.transform.parent = transform;
			cloneObject.SetActive (false);
		}
	}

	public GameObject SpawnObject(Vector2 position) {
		GameObject spawnedObject = GetPooledObject();
		if (spawnedObject != null) {
			spawnedObject.SetActive (true);
			spawnedObject.transform.position = position;
		}

		return spawnedObject;
	}

	GameObject GetPooledObject() {
		foreach (GameObject cloneObject in predefinedObjects) {
			if (!cloneObject.activeInHierarchy) {
				return cloneObject;
			}
		}

		if (isGrowing) {
			GameObject cloneObject = Instantiate(predefinedObjects[0], Vector2.zero, Quaternion.identity) as GameObject;
			return cloneObject;
		}

		return null;
	}

	public static ObjectPooler GetPool(PooledType type) {
		if (objectPoolers.ContainsKey (type)) {
			return objectPoolers [type];
		}
		Debug.LogError (type.ToString () + " pool is absent");
		return null;
	}
}
