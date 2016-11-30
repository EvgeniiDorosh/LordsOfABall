using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleEnemy : MonoBehaviour {
	
	void Awake() {
		MembersAccount.Add (Member.Enemy, gameObject);
	}

	void OnDestroy() {
		MembersAccount.Remove (Member.Enemy, gameObject);
	}
}
