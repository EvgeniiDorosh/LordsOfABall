public interface ICreature {

	float GetCurrentValue (string paramName);
	float GetInitialValue (string paramName);

	void ChangeParameter (string paramName, float diffValue);
}
