using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	void Awake() {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		//Cursor.visible = false;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
