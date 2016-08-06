using UnityEngine;
using System.Collections;

public interface IAttacker {

	float GetDamage (BaseCreatureParameters targetParameters);
}
