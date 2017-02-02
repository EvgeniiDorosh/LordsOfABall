using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour 
{	
	AudioSource audioSource;
	[SerializeField]
	GameObject knockLight;
	[SerializeField]
	AudioClip knockSound;
	Pool knockLightPool;

	public Death death;

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

	public void Demolish()  
	{
		if (death != null) 
		{
			death.ShowDeath ();
		}
		Destroy (gameObject);
	}

	void OnDisable() 
	{
		MembersAccount.Remove (Member.Ball, gameObject);
		Messenger.Invoke(BallEvent.ballWasDestroyed);
	}
}
