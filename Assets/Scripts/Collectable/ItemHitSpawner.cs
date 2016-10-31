using UnityEngine;
using System.Collections;

public class ItemHitSpawner : MonoBehaviour {

	public GameObject[] spawnedItems;

	[Range(1, 300)]
	public int reloadingTime = 1;

	private bool readyToSpawn = true;

	void OnCollisionEnter2D(Collision2D other) {
		if (!readyToSpawn) {
			return;
		}

		IAttacker attacker = other.gameObject.GetComponent<IAttacker> ();
		if (attacker != null) {
			readyToSpawn = false;
			Spawn();
			StartCoroutine (Reload ());
		}
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

	void OnDestroy() {
		StopAllCoroutines ();
	}
}
