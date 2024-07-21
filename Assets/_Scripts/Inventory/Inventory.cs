using System;
using System.Collections.Generic;
using UnityEngine;
using PirateJam.Inventory.UI_Related;
using static UnityEditor.Progress;
using UnityEngine.Serialization;

namespace PirateJam.Inventory
{
    public class Inventory : MonoBehaviour
    {
        [FormerlySerializedAs("items"), SerializeField] private List<Item> startingItems;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private ItemSlot[] itemSlots;

        public event Action<Item> OnItemRightClickedEvent;

        private void Start()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
            }

            SetStartingItems();
        }

        private void OnValidate()
        {
            if (itemsParent != null) 
                itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

            SetStartingItems();
        }

        private void SetStartingItems()
        {
            int i = 0;
            for (; i < startingItems.Count && i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = startingItems[i];
            }

            for (; i < itemSlots.Length; i++)
            {
                itemSlots[i].Item = null;
            }
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == null)
                { 
                    itemSlots[i].Item = item;
                    return true;
                }
            }

            return false;
        }

        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == item)
                {
                    itemSlots[i].Item = null;
                    return true;
                }
            }

            return false;
        }

        public bool IsFull()
        {
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if (itemSlots[i].Item == null)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
