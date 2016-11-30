using UnityEngine;
using System.Collections;

public class WeaponSwitcher : MonoBehaviour {

	public GameObject[] weapons;
	private int currentIndex = 0;

	void Awake () {
		SetActiveWeapon ();
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			currentIndex = (int)Mathf.Repeat (currentIndex + 1, weapons.Length);
			SetActiveWeapon ();
		}
	}

	void SetActiveWeapon() {
		for (int weaponIndex = 0; weaponIndex < weapons.Length; weaponIndex++) {
			if (weaponIndex == currentIndex) {
				weapons [weaponIndex].SetActive (true);
			} else {
				weapons [weaponIndex].SetActive (false);
			}
		}
	}
}
