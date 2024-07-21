using System;
using System.Collections.Generic;
using UnityEngine;
using PirateJam.Inventory.UI_Related;

namespace PirateJam.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> items;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private ItemSlot[] itemSlots;

        public event Action<Item> OnItemRightClickedEvent;

        private void Start()
        {
            if (itemsParent != null)
                itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

            if (itemSlots != null)
            {
                for (int i = 0; i < itemSlots.Length; i++)
                {
                    if (OnItemRightClickedEvent != null)
                        itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
                    else
                        Debug.LogError("OnItemRightClickedEvent is null during assignment.");
                }
            }
            else
            {
                Debug.LogError("Item slots not assigned.");
            }
        }

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

        public bool AddItem(Item item)
        {
            if (IsFull()) return false;

            items.Add(item);
            RefreshUI();
            return true;
        }

        public bool RemoveItem(Item item)
        {
            if (items.Remove(item))
            {
                RefreshUI();
                return true;
            }

            return false;
        }

        public bool IsFull()
        {
            return items.Count >= itemSlots.Length; 
        }
    }
}
