using UnityEngine;

namespace PirateJam.Inventory
{
    public enum EquipmentType
    {
        Weapon1,
        Weapon2,
        Accessory1,
        Accessory2
    }

    [CreateAssetMenu]
    public class EquipableItem : Item
    {
        public int StrengthBonus;
        public int SpeedBonus;

        [Space]
        public float StrengthPercentBonus;
        public float SpeedPercentBonus;

        [Space]
        public EquipmentType EquipmentType;
    }
}