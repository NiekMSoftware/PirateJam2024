using PirateJam.Inventory_System;

namespace PirateJam.Crafting_System
{
    public interface IItemContainer
    {
        bool ContainsItem(Item item);
        int ItemCount(Item item);
        bool RemoveItem(Item item);
        bool AddItem(Item item);
        bool IsFull();
    }
}