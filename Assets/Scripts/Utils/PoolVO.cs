using System;
using UnityEngine;

[Serializable]
public class PoolVO
{
	public int type;
	public GameObject target;
	public int size = 1;

	public PoolVO (GameObject target, int size)
	{
		this.target = target;
		this.size = size;
		this.type = target.GetInstanceID ();
	}

	override public string ToString()
	{
		return string.Format ("Type = {0}, Size = {1}, Name = {2}", target.GetInstanceID(), size, target.name);
	}
}

