using UnityEngine;
using System;
using System.Collections;

[Serializable]
public abstract class Spell : MonoBehaviour {

	public string itemName;

	private float startTime;
	public float CurrentDuration {
		get { return Time.time - startTime; }
	}

	public TemporalType temporalType;
	public enum TemporalType { Temporary, Permanent, Single	}

	public Duration duration;

	[Serializable]
	public class Duration {
		public int minutes;
		public int seconds;

		public int InSeconds {
			get { return minutes * 60 + seconds; }
		}
	}

	protected GameObject targetObject;

	public virtual void Trigger(GameObject targetObject) {
		this.targetObject = targetObject;
		startTime = Time.time;
		Cast ();
		switch (temporalType) {
		case TemporalType.Temporary:
			StartCoroutine (InAction ());
			break;
		case TemporalType.Single:
			Finish ();
			Destroy (gameObject);
			break;
		}
	}

	protected IEnumerator InAction() {
		yield return new WaitForSeconds(duration.InSeconds);
		Finish ();
		Destroy (gameObject);
	}

	public abstract void Cast ();
	public abstract void Finish ();
}
