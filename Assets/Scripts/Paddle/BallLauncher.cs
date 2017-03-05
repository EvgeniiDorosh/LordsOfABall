using UnityEngine;
using System;
using System.Collections;

public class BallLauncher : MonoBehaviour 
{
	[SerializeField]
	Transform platform;
	[SerializeField]
	Transform ballInitialSpot;
	[SerializeField]
	GameObject ball;
	GameObject caughtBall;

	StatsController statsController;
	BoxCollider2D platformCollider;
	Vector2 defaultColliderSize;

	bool hasBall = true;
	float reflectionFactor;

	static string fireKey = "Fire1";

	public bool CatchingIsActive { get; set; }

	public float ReflectionFactor 
	{ 
		get { return reflectionFactor;}
		set { reflectionFactor = Mathf.Clamp(value, 0.1f, 0.9f); }
	}

	void UpdateWidth(BaseStat stat) 
	{
		platform.transform.localScale = new Vector3 (stat.Value, 1f, 1f);
		platformCollider.size = new Vector2(defaultColliderSize.x * stat.Value, defaultColliderSize.y);
	}

	void Awake() 
	{		
		statsController = GetComponent<StatsController> ();
		platformCollider = GetComponent<BoxCollider2D> ();
		defaultColliderSize = platformCollider.size;
		ReflectionFactor = 0.5f;
		SetupBall ();
	}

	void Start() 
	{
		statsController.Get<Stat> (StatType.Width).ValueChanged += UpdateWidth;
	}

	void OnEnable()
	{
		Messenger.AddListener(BallEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
	}

	void OnDisable() 
	{
		CancelInvoke ();
		Messenger.RemoveListener(BallEvent.ballWasDestroyed, CheckAllBallAreDestroyed);
	}
	
	void Update () 
	{
		if (Input.GetButton (fireKey) && hasBall) 
		{
			LaunchBall (caughtBall);
			hasBall = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.CompareTag ("Ball")) 
		{
			if (CatchingIsActive && !hasBall) 
			{
				hasBall = true;
				CatchBall (collision.gameObject);
			} 
			else 
			{
				LaunchBall (collision.gameObject);
			}
		}
	}

	void CheckAllBallAreDestroyed() 
	{
		if (MembersAccount.Count(Member.Ball) == 0) 
		{
			Invoke("SetupBall", 0.5f);		
		}
	}

	void SetupBall() 
	{
		caughtBall = Instantiate (ball, ballInitialSpot.position, Quaternion.identity, transform) as GameObject;
		hasBall = true;
	}

	void LaunchBall(GameObject ball) 
	{
		float xPos = HitFactor(ball.transform.position);
		Vector2 direction = new Vector2(xPos, reflectionFactor).normalized;
		ball.GetComponent<BallMotionController> ().Launch (direction);
	}

	void CatchBall(GameObject ball) 
	{
		caughtBall = ball;
		caughtBall.GetComponent<BallMotionController> ().Stop ();
		caughtBall.transform.SetParent (transform);
	}

	float HitFactor(Vector2 hitPosition) 
	{
		return (hitPosition.x - transform.position.x) / platformCollider.bounds.size.x;
	}
}
