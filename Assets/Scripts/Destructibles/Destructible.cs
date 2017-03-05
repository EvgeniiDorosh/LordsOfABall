using UnityEngine;

public class Destructible : MonoBehaviour 
{
	void Awake() 
	{
		MembersAccount.Add (Member.Destructible, gameObject);
	}

	void OnDestroy() 
	{
		MembersAccount.Remove (Member.Destructible, gameObject);
	}
}
