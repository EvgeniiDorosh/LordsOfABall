using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleEnemy : MonoBehaviour 
{
	void OnEnable()
	{
		MembersAccount.Add (Member.Enemy, gameObject);
	}

	void OnDisable() 
	{
		MembersAccount.Remove (Member.Enemy, gameObject);
	}
}
