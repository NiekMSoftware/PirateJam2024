using UnityEngine;

namespace PirateJam.Inventory
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private EquipmentPanel equipmentPanel;

        private void Awake()
        {
            inventory.OnItemRightClickedEvent += EquipFromInventory;
            equipmentPanel.OnItemRightClickedEvent += UnequipFromPanel;
        }

        private void EquipFromInventory(Item item)
        {
            if (item is EquipableItem)
            {
                Equip((EquipableItem)item);
            }
        }

        private void UnequipFromPanel(Item item)
        {
            if (item is EquipableItem) { 
                Unequip((EquipableItem)item);
            }
        }

        public void Equip(EquipableItem item)
        {
            if (inventory.RemoveItem(item))
            {
                EquipableItem previousItem;
                if (equipmentPanel.AddItem(item, out previousItem))
                {
                    if (previousItem != null)
                    {
                        inventory.AddItem(previousItem);
                    }
                }
                else
                {
                    inventory.AddItem(item);
                }
            }
        }

        public void Unequip(EquipableItem item)
        {
            if (!inventory.IsFull() && equipmentPanel.RemoveItem(item)) 
            {
                inventory.AddItem(item);
            }
        }
    }
}