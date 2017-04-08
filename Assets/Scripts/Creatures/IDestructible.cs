using System;
using UnityEngine;

public interface IDestructible
{
	event DestructDelegate Destructed;
	void Destruct();
}

public delegate void DestructDelegate(GameObject target);