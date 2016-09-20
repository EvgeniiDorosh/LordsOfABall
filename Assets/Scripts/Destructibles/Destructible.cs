using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Destructible : Damageable {

	public static readonly List<GameObject> destructibles = new List<GameObject>();
}
