public class StatChange {

	public string name;
	public float initial;
	public float current;
	public float diff;

	public StatChange(string name, float initial, float current, float diff) {
		this.name = name;
		this.initial = initial;
		this.current = current;
		this.diff = diff;
	}

	public float LastValue {
		get { return current - diff; }
	}

	public float RelativeDiff {
		get { return current / LastValue; }
	}
}
