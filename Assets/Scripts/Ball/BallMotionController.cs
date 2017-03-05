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
		get { return speed.Value; }
	}

	public void ChangeDirection(Vector2 direction) 
	{
		Debug.Log ("Rigidbody = " + rigidBody + "; speed = " + speed);
		rigidBody.velocity = direction.normalized * speed.Value;
	}

	public void Launch(Vector2 direction) 
	{
		transform.parent = null;
		rigidBody.isKinematic = false;
		ChangeDirection(direction);
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
		StopAllCoroutines ();
		StartCoroutine (SpeedChanging (4));
	}

	IEnumerator SpeedChanging(float time)
	{
		float timeStep = 0.5f;
		WaitForSeconds waiting = new WaitForSeconds (timeStep);
		float currentSpeed = rigidBody.velocity.magnitude;
		float targetSpeed = speed.Value;
		float stepValue = Mathf.Abs (targetSpeed - currentSpeed) * timeStep / time;
		while (currentSpeed != targetSpeed) 
		{
			currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, stepValue);
			rigidBody.velocity = rigidBody.velocity.normalized * currentSpeed;
			yield return waiting;
		}
		StopAllCoroutines ();
	}	
}
