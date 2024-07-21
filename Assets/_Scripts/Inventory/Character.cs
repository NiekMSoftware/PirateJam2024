using UnityEngine;
using PirateJam.CharacterStats;
using PirateJam.Inventory.UI_Related;

namespace PirateJam.Inventory
{
    public class Character : MonoBehaviour
    {
        public CharacterStat Strength;
        public CharacterStat Agility;
        public CharacterStat Vitality;

        [Space]
        [SerializeField] private Inventory inventory;
        [SerializeField] private EquipmentPanel equipmentPanel;
        [SerializeField] private StatPanel statPanel;

        private void Awake()
        {
            statPanel.SetStats(Strength, Agility, Vitality);
            statPanel.UpdateStatValues();
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
                        previousItem.Unequip(this);
                        statPanel.UpdateStatValues();
                    }

                    item.Equip(this);
                    statPanel.UpdateStatValues();
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
                item.Unequip(this);
                statPanel.UpdateStatValues();

                inventory.AddItem(item);
            }
        }
    }
}