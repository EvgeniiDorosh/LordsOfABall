using UnityEngine;
using System.Collections;

public class ItemHitSpawner : MonoBehaviour {

	public GameObject[] spawnedItems;

	[Range(1, 300)]
	public int reloadingTime = 1;

	private bool readyToSpawn = true;

	void OnEnable() {
		string destinationEvent = CreatureEvent.creatureGotDamage + gameObject.GetInstanceID ();
		Messenger<float>.AddListener (destinationEvent, OnGetDamage);
	}

	void OnDisable() {
		string destinationEvent = CreatureEvent.creatureGotDamage + gameObject.GetInstanceID ();
		Messenger<float>.RemoveListener (destinationEvent, OnGetDamage);
		StopAllCoroutines ();
	}

	void OnGetDamage(float damage) {
		if (!readyToSpawn) {
			return;
		}

		readyToSpawn = false;
		Spawn();
		StartCoroutine (Reload ());
	}

	void Spawn() {
		GameObject spawnedItem = Instantiate(spawnedItems [Random.Range (0, spawnedItems.Length)], transform.position, Quaternion.identity) as GameObject;
		spawnedItem.SetActive(true);
	}

	IEnumerator Reload() {
		yield return new WaitForSeconds (reloadingTime);
		readyToSpawn = true;
		StopCoroutine (Reload ());
	}
}
