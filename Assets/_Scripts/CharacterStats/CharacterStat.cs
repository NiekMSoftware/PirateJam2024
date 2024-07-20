using System.Collections.Generic;

namespace PirateJam.CharacterStats
{
    public class CharacterStat
    {
        public float BaseValue;

        private readonly List<StatModifier> statModifiers;

        public CharacterStat(float baseValue)
        {
            BaseValue = baseValue;
            statModifiers = new List<StatModifier>();
        }
    }
}