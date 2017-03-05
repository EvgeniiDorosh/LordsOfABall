public interface ICreature 
{
	float GetStatValue (StatType type);
	void  ChangeStatValue (StatType type, float diffValue);
}
