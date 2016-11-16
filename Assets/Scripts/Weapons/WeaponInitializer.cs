using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(WeaponParametersController))]
public class WeaponInitializer : MonoBehaviour {

	private WeaponParametersController paramsController;
	public string weaponName;

	void OnEnable () {
		paramsController = GetComponent<WeaponParametersController> ();
		paramsController.InitialParameters = ConfigsParser.instance.weaponsConfig.GetParametersByName (weaponName);
		paramsController.CurrentParameters = paramsController.InitialParameters.Clone();
	}
}
