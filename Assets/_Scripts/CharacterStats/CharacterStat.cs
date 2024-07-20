using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PirateJam.CharacterStats
{
    public class CharacterStat
    {
        public float BaseValue;

        public float Value {
            get {
                if (isDirty || BaseValue != lastBaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }

                return _value;
            }
        }

        private bool isDirty = true;
        private float _value;
        private float lastBaseValue = float.MinValue;

        private readonly List<StatModifier> statModifiers;
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat(float baseValue)
        {
            BaseValue = baseValue;
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();
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
            if (statModifiers.Remove(modifier))
            {
                isDirty = true;
                return true;
            }

            return false;
        }

        public bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        private float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++) 
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModifierType.Flat)
                { 
                    finalValue += mod.Value; 
                }
                else if (mod.Type == StatModifierType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 > statModifiers.Count || statModifiers[i + 1].Type != StatModifierType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0f;
                    }
                }
                else if (mod.Type == StatModifierType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;    //< if base is 10, and increase is 10% then the final value will be 11 (10 * 1.1).
                }
            }

            // 12.0001f != 12f
            return (float)Math.Round(finalValue, 4);
        }
    }
}