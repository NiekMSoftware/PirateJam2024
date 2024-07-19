using System.Collections.Generic;
using UnityEngine;

namespace PirateJam.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> items;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private ItemSlot[] itemSlots;

        private void OnValidate()
        {
            if (itemsParent != null) 
                itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

            RefreshUI();
        }

        private void RefreshUI()
        {
            int i = 0;
            for (; i < items.Count && i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = items[i];
            }

            for (; i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = null;
            }
        }
    }
}
