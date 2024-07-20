using UnityEngine;
using PirateJam.CharacterStats;

namespace PirateJam.Inventory
{
    public enum EquipmentType
    {
        Weapon1,
        Accessory1,
        Accessory2
    }

    [CreateAssetMenu]
    public class EquipableItem : Item
    {
        public int StrengthBonus;
        public int AgilityBonus;
        public int VitalityBonus;

        [Space]
        public float StrengthPercentBonus;
        public float AgilityPercentBonus;
        public float VitalityPercentBonus;

        [Space]
        public EquipmentType EquipmentType;

        public void Equip(Character c)
        {
            // Flat bonuses
            if (StrengthBonus != 0)
                c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModifierType.Flat, this));
            if (AgilityBonus != 0)
                c.Agility.AddModifier(new StatModifier(AgilityBonus, StatModifierType.Flat, this));
            if (VitalityBonus != 0)
                c.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModifierType.Flat, this));

            // percentile bonuses
            if (StrengthPercentBonus != 0)
                c.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModifierType.PercentMult, this));
            if (AgilityPercentBonus != 0)
                c.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModifierType.PercentMult, this));
            if (VitalityPercentBonus != 0)
                c.Vitality.AddModifier(new StatModifier(VitalityPercentBonus, StatModifierType.PercentMult, this));
        }

        public void Unequip(Character c)
        {
            c.Strength.RemoveAllModifiersFromSource(this);
            c.Agility.RemoveAllModifiersFromSource(this);
            c.Vitality.RemoveAllModifiersFromSource(this);
        }
    }
}