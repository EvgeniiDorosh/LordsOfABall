using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainPreloader : MonoBehaviour {

	public Slider slider;
	private Rigidbody2D ballRigidBody;
	private AsyncOperation loadOperation;

	void Start () {
		ballRigidBody = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Rigidbody2D>();
		StartCoroutine (Delay());
	}
	
	void Update () {
		ballRigidBody.drag += 0.2f * Time.deltaTime;
		if (loadOperation != null) {
			slider.value = loadOperation.progress;
		}
	}

	IEnumerator Delay() {
		yield return new WaitForSeconds (3f);
		LoadGame ();
	}

	void LoadGame () {
		loadOperation = SceneManager.LoadSceneAsync (1);
	}
}
