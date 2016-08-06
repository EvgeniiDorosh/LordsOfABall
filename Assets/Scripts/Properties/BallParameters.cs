using UnityEngine;
using System;

[Serializable]
public class BallParameters : BaseCreatureParameters {

	/**
	 * Speed
	 */
	[SerializeField]
	[Range(0, 500)]
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
	[SerializeField]
	[Range(0, 500)]
	private float luck;

	public float Luck {
	
		get { 
			return luck;
		}
		set { 
			luck = value;		
		}
	}

	public override BallParameters Clone<BallParameters>() {
		BallParameters result = base.Clone<BallParameters> ();


		return result;
	}
}
