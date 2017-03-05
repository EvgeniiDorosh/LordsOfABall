using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour 
{
	[SerializeField]
	GameObject paddle;
	GameObject clonePaddle;

	static Rect borders;
	static LevelManager instance = null;

	public static LevelManager Instance 
	{
		get { return instance;}
	}

	public static Rect Borders
	{
		get { return borders;}
	}

	void Awake () 
	{
		if (instance != null) 
		{
			Destroy (gameObject);
			return;
		}
		instance = this;
		DefineBorders ();
		clonePaddle = Instantiate (paddle, Vector2.zero, Quaternion.identity) as GameObject;
		//Time.timeScale = 0.5f;
	}

	void OnEnable() 
	{
		Cursor.visible = false;
		AddListeners ();
	}

	void OnDisable() 
	{
		Cursor.visible = true;
		RemoveListeners ();
	}

	void AddListeners() {
		Messenger.AddListener (LevelEvent.allEnemiesAreDestroyed, OnLevelComplete);
	}

	void RemoveListeners() {
		Messenger.RemoveListener (LevelEvent.allEnemiesAreDestroyed, OnLevelComplete);
	}

	void OnLevelComplete() {
		StartCoroutine (CompletingLevel ());
	}

	void StopPaddle() {
		clonePaddle.GetComponentInChildren<BallLauncher> ().enabled = false;
	}

	void StopBalls() {
		int ballsCount = MembersAccount.Count (Member.Ball);
		if (ballsCount > 0) {
			List<GameObject> balls = MembersAccount.Get (Member.Ball);
			for (int index = 0; index < ballsCount; index++) {
				balls[index].GetComponent<BallMotionController> ().Stop ();
			}
		}
	}

	IEnumerator CompletingLevel() {
		StopPaddle ();
		StopBalls ();
		while (MembersAccount.Count (Member.Ball) != 0) {
			Ball ball = MembersAccount.Get (Member.Ball) [0].GetComponent<Ball> ();
			ball.Demolish ();
			yield return new WaitForSeconds (0.5f);
		}
		while (InitialParameterSpell.createdInitialSpells.Count != 0) {
			yield return new WaitForSeconds (2f);		
		}
		yield return new WaitForSeconds (1f);
		StopAllCoroutines ();
		Messenger.Invoke (LevelEvent.levelIsComplete);
	}

	void DefineBorders()
	{
		GameObject[] borderGameObjects = GameObject.FindGameObjectsWithTag ("PaddleEdge");
		int length = borderGameObjects.Length;
		float[] xValues = new float[length];
		float[] yValues = new float[length];
		for (int index = 0; index < length; index++) 
		{
			xValues [index] = borderGameObjects[index].transform.position.x;
			yValues [index] = borderGameObjects[index].transform.position.y;
		}

		borders = new Rect ();
		borders.xMin = Mathf.Min (xValues);
		borders.xMax = Mathf.Max (xValues);
		borders.yMin = Mathf.Min (yValues);
		borders.yMin = Mathf.Max (yValues);
	}
}
