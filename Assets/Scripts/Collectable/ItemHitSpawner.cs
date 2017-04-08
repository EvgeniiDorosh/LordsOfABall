using UnityEngine;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class ItemHitSpawner : MonoBehaviour 
{
	[SerializeField]
	GameObject[] spawnedItems;

	[SerializeField]
	[Range(1, 300)]
	int reloadingTime = 1;

	bool readyToSpawn = true;
	IDamageable target;

	void Awake()
	{
		target = GetComponent<IDamageable> ();
	}

	void OnEnable() 
	{
		if (target != null)
			target.GotDamage += OnGetDamage;
	}

	void OnDisable() 
	{
		if (target != null)
			target.GotDamage -= OnGetDamage;
		StopAllCoroutines ();
	}

	void OnGetDamage(object sender, EventArgs e) 
	{
		if (!readyToSpawn)
			return;
		
		readyToSpawn = false;
		Spawn();
		StartCoroutine (Reload ());
	}

	void Spawn() 
	{
		GameObject spawnedItem = Instantiate(spawnedItems [Random.Range (0, spawnedItems.Length)], transform.position, Quaternion.identity) as GameObject;
		spawnedItem.SetActive(true);
	}

	IEnumerator Reload() 
	{
		yield return new WaitForSeconds (reloadingTime);
		readyToSpawn = true;
		StopCoroutine (Reload ());
	}
}