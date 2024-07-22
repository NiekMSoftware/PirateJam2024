using PirateJam.Inventory_System.UI_Related;
using PirateJam.Inventory_System;
using UnityEngine;

namespace PirateJam.Crafting_System.UI_Related
{
    public class CraftingRecipeUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform arrowParent;
        [SerializeField] private BaseItemSlot[] itemSlots;

        [Header("Public Variables")]
        public ItemContainer ItemContainer;
    }
}