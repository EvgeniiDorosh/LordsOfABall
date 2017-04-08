using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour, IDestructible
{	
	AudioSource audioSource;
	[SerializeField]
	GameObject knockLight;
	[SerializeField]
	AudioClip knockSound;
	Pool knockLightPool;

	// TODO: Remove this;
	[SerializeField]
	Death death;

	public event DestructDelegate Destructed;

	public void Destruct()  
	{
		if (death != null) 
			death.ShowDeath ();
		if (Destructed != null)
			Destructed (gameObject);
		Destroy (gameObject);
	}

	void Awake () 
	{	
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = knockSound;
	}

	void OnEnable() 
	{
		MembersAccount.Add (Member.Ball, gameObject);
		knockLightPool = ObjectPooler.CreatePool (new PoolVO (knockLight, 10));
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		audioSource.Play ();
		knockLightPool.SpawnObject (other.contacts[0].point);
	}

	void OnDisable() 
	{
		MembersAccount.Remove (Member.Ball, gameObject);
		Messenger.Invoke(BallEvent.ballWasDestroyed);
	}
}
