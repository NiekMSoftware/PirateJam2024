using UnityEngine;

namespace PirateJam.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private EquipmentPanel equipmentPanel;

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
    }
}