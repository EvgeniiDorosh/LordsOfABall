using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

	private static readonly Dictionary<PooledType, ObjectPooler> objectPoolers = new Dictionary<PooledType, ObjectPooler>();

	public int size = 0;
	public PooledType pooledType;
	public GameObject targetObject;

	[Tooltip("If filled, target object will be ignored")]
	public GameObject[] predefinedObjects;

	private Queue<GameObject> pooledQueue = new Queue<GameObject> ();

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
		if (predefinedObjects.Length > 0) {
			size = predefinedObjects.Length;
		} else {
			predefinedObjects = new GameObject[size];
			for (int i = 0; i < size; i++) {
				predefinedObjects[i] = Instantiate(targetObject, Vector2.zero, Quaternion.identity) as GameObject;
			}
		}

		for (int i = 0; i < size; i++) {
			GameObject pooledObject = predefinedObjects[i];
			pooledObject.transform.parent = transform;
			pooledQueue.Enqueue (predefinedObjects[i]);
			pooledObject.SetActive (false);
		}
	}

	public GameObject SpawnObject(Vector2 position) {
		GameObject spawnedObject = pooledQueue.Dequeue ();
		spawnedObject.SetActive (true);
		spawnedObject.transform.position = position;
		pooledQueue.Enqueue (spawnedObject);

		return spawnedObject;
	}

	public static ObjectPooler GetPool(PooledType type) {
		if (objectPoolers.ContainsKey (type)) {
			return objectPoolers [type];
		}
		Debug.LogError (type.ToString () + " pool is absent");
		return null;
	}
}
