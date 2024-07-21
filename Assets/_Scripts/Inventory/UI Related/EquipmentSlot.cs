using UnityEngine;

namespace PirateJam.Inventory.UI_Related
{
    public class EquipmentSlot : ItemSlot
    {
        public EquipmentType EquipmentType;

        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.name = EquipmentType.ToString() + " Slot";
        }
    }
}