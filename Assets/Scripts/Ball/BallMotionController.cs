using UnityEngine;
using System;
using System.Collections;

public class BallMotionController : MonoBehaviour
{
	Stat speed;
    StatsController statsController;
	Rigidbody2D rigidBody;

	public float Speed
	{
		get 
		{ 
			if (speed == null) 
				speed = statsController.Get<Stat> (StatType.Speed);
			return speed.Value; 
		}
	}

	public void Launch(Vector2 direction) 
	{
		transform.parent = null;
		rigidBody.isKinematic = false;
		rigidBody.velocity = direction.normalized * Speed;
	}

	public void Stop() 
	{
		rigidBody.isKinematic = true;
		rigidBody.velocity = Vector2.zero;
	}

	void Awake() 
	{
		statsController = GetComponent<StatsController> ();
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Start () 
	{
		speed = statsController.Get<Stat> (StatType.Speed);
		speed.ValueChanged += OnSpeedChanged;
	}

	void OnSpeedChanged(BaseStat stat) 
	{
		if (rigidBody.isKinematic)
			return;
		rigidBody.velocity = rigidBody.velocity.normalized * Speed;
	}
}
