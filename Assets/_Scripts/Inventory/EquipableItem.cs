using UnityEngine;

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
    }
}