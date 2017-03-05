using UnityEngine;
using System.Collections;
using System;

public class PaddleMotionController : MonoBehaviour 
{	
	static string mouseXAxis = "Mouse X";

	[SerializeField]
	float moveSpeed = 0.8f;

	Rect allowedBorders;

	Collider2D platformCollider;
	StatsController statsController;

	bool isInverseMotion = false;
	public bool IsInverseMotion 
	{
		get { return isInverseMotion;}
		set { isInverseMotion = value;}
	}

	void Awake() 
	{
		platformCollider = GetComponent<Collider2D> ();
		statsController = GetComponent<StatsController> ();
		transform.position = GetInitialPosition();
	}

	void Start() 
	{
		allowedBorders = GetPaddleAllowedBorders ();
		statsController.Get<Stat>(StatType.Width).ValueChanged += OnWidthChanged;
	}
	
	void Update () 
	{
		float mouseMoveX = moveSpeed * Input.GetAxis (mouseXAxis);
		float playerX = transform.position.x + (isInverseMotion ? -1 : 1) * mouseMoveX;
		Vector2 playerPosition = new Vector2 (Mathf.Clamp (playerX, allowedBorders.xMin, allowedBorders.xMax), transform.position.y);
		transform.position = playerPosition;
	}

	Rect GetPaddleAllowedBorders() 
	{
		float paddleOffset = platformCollider.bounds.size.x / 2;

		Rect borders = new Rect ();
		borders.xMin = LevelManager.Borders.xMin + paddleOffset;
		borders.xMax = LevelManager.Borders.xMax - paddleOffset;

		return borders;
	}

	Vector2 GetInitialPosition() 
	{
		float yPosition = Camera.main.ScreenToWorldPoint (Vector2.zero).y + 0.5f;
		float xPosition = (allowedBorders.xMax - allowedBorders.xMin) / 2;
		return new Vector2 (xPosition, yPosition);
	}

	void OnWidthChanged(BaseStat stat) 
	{
		allowedBorders = GetPaddleAllowedBorders ();
	}
}
