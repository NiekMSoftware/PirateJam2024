using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PirateJam.Inventory
{
    public class EquipmentPanel : MonoBehaviour
    {
        [SerializeField] private Transform equipmentSlotsParent;
        [SerializeField] private EquipmentSlot[] equipmentSlots;

        private void OnValidate()
        {
            equipmentSlots = GetComponentsInChildren<EquipmentSlot>();
        }

        public bool AddItem(EquipableItem item, out EquipableItem previousItem)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].EquipmentType == item.EquipmentType)
                {
                    previousItem = (EquipableItem)equipmentSlots[i].Item;
                    equipmentSlots[i].Item = item;
                    return true;
                }
            }

            previousItem = null;
            return false;
        }

        public bool RemoveItem(EquipableItem item)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].Item == item)
                {
                    equipmentSlots[i].Item = null;
                    return true;
                }
            }

            return false;
        }
    }
}