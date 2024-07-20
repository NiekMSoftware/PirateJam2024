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
            equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
        }
    }
}