using UnityEngine;
using System.Collections;

public class BallParameters : BaseCreatureParameters {

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

	/**
	 * Luck
	 */ 

	private float luck;

	public float Luck {
	
		get { 
			return luck;
		}
		set { 
			luck = value;		
		}
	}
}
