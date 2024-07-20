namespace PirateJam.CharacterStats
{
    public enum StatModifierType
    {
        Flat,
        Percent
    }

    public class StatModifier
    {
        public readonly float Value;
        public readonly StatModifierType Type;

        public StatModifier(float value, StatModifierType type)
        {
            Value = value;
            Type = type;
        }
    }
}