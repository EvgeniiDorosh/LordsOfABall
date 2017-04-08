using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class MembersAccount : MonoBehaviour
{
	static Dictionary<Member, List<GameObject>> members = new Dictionary<Member, List<GameObject>>();
	MembersAccount instance = null;

	public static void Add(Member key, GameObject target) 
	{
		if (!members.ContainsKey (key)) 
			members [key] = new List<GameObject> ();
		members [key].Add (target);
	}

	public static void Remove(Member key, GameObject target) 
	{
		if (members.ContainsKey (key)) 
			members [key].Remove(target);
	}

	public static List<GameObject> Get(Member key) 
	{
		if (members.ContainsKey (key)) 
			return members [key];
		return null;
	}

	public static int Count(Member key)
	{
		if (members.ContainsKey (key)) 
			return members [key].Count;
		return 0;
	}

	void Awake()
	{
		if (instance != null) 
		{
			Destroy (gameObject);
			return;
		}
		instance = this;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	void OnSceneUnloaded(Scene scene)
	{
		Clear ();
	}

	void Clear() 
	{
		foreach (KeyValuePair<Member, List<GameObject>> member in members) 
			member.Value.Clear ();	
	}
}

