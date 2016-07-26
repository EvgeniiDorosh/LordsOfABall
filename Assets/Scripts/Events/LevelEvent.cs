using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class LevelEvent : UnityEvent<Object> {

	public const string ballWasDestroyed = "ballWasDestroyed";
}
