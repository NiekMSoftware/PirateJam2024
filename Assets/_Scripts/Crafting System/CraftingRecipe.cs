using System;
using System.Collections.Generic;
using UnityEngine;
using PirateJam.Inventory_System;

namespace PirateJam.Crafting_System
{
    [Serializable]
    public struct ItemAmount
    {
        public Item Item;

        [Range(1, 999)] 
        public int Amount;
    }

    [CreateAssetMenu]
    public class CraftingRecipe : ScriptableObject
    {
        public List<ItemAmount> Materials;
        public List<ItemAmount> Results;

        public bool CanCraft(Inventory inventory)
        {
            return false;
        }
    }
}