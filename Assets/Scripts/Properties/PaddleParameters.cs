using UnityEngine;
using System;

[Serializable]
public class PaddleParameters : BaseCreatureParameters {

	/**
	 * Width
	 */ 

	private float width;

	public float Width {

		get {
			return width;
		}
		set { 
			width = value < 0 ? 0 : value;
		}
	}

	/**
	 * Speed
	 */ 

	private float speed;

	public float Speed {

		get {
			return speed;
		}
		set { 
			speed = value < 0 ? 0 : value;
		}
	}
}
