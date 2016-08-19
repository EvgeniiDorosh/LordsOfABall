using UnityEngine;
using System.Collections;

public interface IAttacker {

	float GetDamage (CreatureParameters targetParameters);
}
