using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainPreloader : MonoBehaviour {

	public Slider slider;
	private Rigidbody2D ballRigidBody;
	private AsyncOperation loadOperation;

	void Start () {
		ballRigidBody = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Rigidbody2D>();
		Invoke ("LoadGame", 2f);
	}
	
	void Update () {
		ballRigidBody.drag += 0.2f * Time.deltaTime;
		if (loadOperation != null) {
			slider.value = loadOperation.progress;
		}
	}

	void LoadGame () {
		loadOperation = SceneManager.LoadSceneAsync (1);
	}
}
