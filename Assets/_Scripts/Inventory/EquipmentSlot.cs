using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateJam.Inventory
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
