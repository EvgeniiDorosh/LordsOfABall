using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MembersAccount {

	private static Dictionary<Member, List<GameObject>> members = new Dictionary<Member, List<GameObject>>();

	public static void Add(Member key, GameObject target) {
		if (!members.ContainsKey (key)) {
			members [key] = new List<GameObject> ();
		}
		members [key].Add (target);
	}

	public static void Remove(Member key, GameObject target) {
		if (members.ContainsKey (key)) {
			members [key].Remove(target);
		}
	}

	public static List<GameObject> Get(Member key) {
		if (members.ContainsKey (key)) {
			return members [key];
		}
		return null;
	}

	public static bool ContainsKey(Member key) {
		return members.ContainsKey (key);
	}

	public static int Count(Member key) {
		if (members.ContainsKey (key)) {
			return members [key].Count;
		}
		return 0;
	}

	public static void Clear(Member key) {
		if (members.ContainsKey (key)) {
			members [key].Clear();
			members.Remove (key);
		}
	}

	public static void Clear() {
		Member[] keys = new Member[members.Keys.Count];
		Member key;
		members.Keys.CopyTo (keys, 0);
		for (int i = 0; i < keys.Length; i++) {
			key = keys [i];
			members [key].Clear();
			members.Remove (key);
		}
	}
}

