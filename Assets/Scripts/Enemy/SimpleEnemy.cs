using UnityEngine;

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
