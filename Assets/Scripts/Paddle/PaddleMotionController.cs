﻿using UnityEngine;
using System.Collections;
using System;

public class PaddleMotionController : MonoBehaviour {

	public BoxCollider2D platformCollider;

	private Edges allowedEdges;

	[Serializable]
	private class Edges {

		public float xMin;
		public float xMax;

		public Edges(float xMin, float xMax) {
			this.xMin = xMin;
			this.xMax = xMax;
		}
	}

	void Start () {
		allowedEdges = GetPaddleAllowedEdges ();
		transform.position = GetInitialPlayerPosition();
	}

	void OnEnable() {
		Messenger.AddListener (PaddleEvent.widthWasUpdated, OnWidthChanged);
	}

	void OnDisable() {
		Messenger.RemoveListener (PaddleEvent.widthWasUpdated, OnWidthChanged);
	}
	
	void Update () {
		float mouseX = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, 
																	Input.mousePosition.y)).x;
		Vector2 playerPosition = new Vector2 (Mathf.Clamp (mouseX, allowedEdges.xMin, allowedEdges.xMax), transform.position.y);
		transform.position = playerPosition;
	}

	Edges GetPaddleAllowedEdges() {
		GameObject[] borderGameObjects = GameObject.FindGameObjectsWithTag ("PaddleEdge");
		int length = borderGameObjects.Length;
		float[] xValues = new float[length];
		for (int index = 0; index < length; index++) {
			xValues [index] = borderGameObjects[index].transform.position.x;
		}

		float paddleOffset = platformCollider.bounds.size.x / 2;

		Edges edges = new Edges (Mathf.Min (xValues) + paddleOffset, Mathf.Max (xValues) - paddleOffset);

		return edges;
	}

	Vector2 GetInitialPlayerPosition() {
		float yPosition = Camera.main.ScreenToWorldPoint (Vector2.zero).y + 0.5f;
		float xPosition = (allowedEdges.xMax - allowedEdges.xMin) / 2;
		Vector2 playerPosition = new Vector2 (xPosition, yPosition);

		return playerPosition;
	}

	void OnWidthChanged() {
		allowedEdges = GetPaddleAllowedEdges ();
	}
}
