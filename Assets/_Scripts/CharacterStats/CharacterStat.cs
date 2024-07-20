using System;
using System.Collections.Generic;

namespace PirateJam.CharacterStats
{
    public class CharacterStat
    {
        public float BaseValue;
        public float Value {
            get {
                if (isDirty)
                {
                    _value = CalculateFinalValue();
                    isDirty = false;
                }

                return _value;
            }
        }

        private bool isDirty = true;
        private float _value;

        private readonly List<StatModifier> statModifiers;

        public CharacterStat(float baseValue)
        {
            BaseValue = baseValue;
            statModifiers = new List<StatModifier>();
        }

        public void AddModifier(StatModifier modifier) 
        { 
            isDirty = true;
            statModifiers.Add(modifier);
            statModifiers.Sort(CompareModifierOrder);
        }

        private int CompareModifierOrder(StatModifier a, StatModifier b) 
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;

            return 0; // if (a.Order == b.Order)
        }

        public bool RemoveModifier(StatModifier modifier)
        {
            isDirty = true;
            return statModifiers.Remove(modifier);
        }

        private float CalculateFinalValue()
        {
            float finalValue = BaseValue;

            for (int i = 0; i < statModifiers.Count; i++) 
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModifierType.Flat)
                { 
                    finalValue += mod.Value; 
                }
                else if (mod.Type == StatModifierType.Percent)
                {
                    finalValue *= 1 + mod.Value;    //< if base is 10, and increase is 10% then the final value will be 11 (10 * 1.1).
                }
            }

            // 12.0001f != 12f
            return (float)Math.Round(finalValue, 4);
        }
    }
}