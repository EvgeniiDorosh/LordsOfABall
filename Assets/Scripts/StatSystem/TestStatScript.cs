using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestStatScript : MonoBehaviour
{
	public List<BaseStat> stats;
	// Use this for initialization
	void Start ()
	{
		foreach (BaseStat stat in stats) {
			Debug.Log (stat.ToString ());
		}
	}
}

