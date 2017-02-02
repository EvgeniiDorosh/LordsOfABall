using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class MembersAccount : MonoBehaviour
{
	MembersAccount instance = null;
	static Dictionary<Member, List<GameObject>> members = new Dictionary<Member, List<GameObject>>();

	void Awake()
	{
		if (instance != null) 
		{
			Destroy (gameObject);
			return;
		}
		instance = this;
		Initialize ();
		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	void Initialize()
	{
		var keys = Enum.GetValues (typeof(Member));
		foreach (Member key in keys) 
		{
			members [key] = new List<GameObject> ();
		}
	}

	void OnSceneUnloaded(Scene scene)
	{
		Clear ();
	}

	public static void Add(Member key, GameObject target) 
	{
		if (!members.ContainsKey (key)) 
		{
			members [key] = new List<GameObject> ();
		}
		members [key].Add (target);
	}

	public static void Remove(Member key, GameObject target) 
	{
		if (members.ContainsKey (key)) 
		{
			members [key].Remove(target);
		}
	}

	public static List<GameObject> Get(Member key) 
	{
		if (members.ContainsKey (key)) 
		{
			return members [key];
		}
		return null;
	}

	public static int Count(Member key)
	{
		if (members.ContainsKey (key)) 
		{
			return members [key].Count;
		}
		return 0;
	}

	void Clear() 
	{
		foreach (KeyValuePair<Member, List<GameObject>> member in members) 
		{
			member.Value.Clear ();	
		}
	}
}

