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

        public bool AddItem(EquippableItem item, out EquippableItem previousItem)
        {
            for (int i = 0; i < equipmentSlots.Length; i++)
            {
                if (equipmentSlots[i].EquipmentType == item.EquipmentType)
                {
                    previousItem = (EquippableItem)equipmentSlots[i].Item;
                    equipmentSlots[i].Item = item;
                    return true;
                }
            }

            previousItem = null;
            return false;
        }

        public bool RemoveItem(EquippableItem item)
        {
            if (item == null) {
                Debug.LogError("No item to remove.");
                return false; 
            }

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