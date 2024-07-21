using UnityEngine;
using PirateJam.CharacterStats;
using PirateJam.Inventory_System.UI_Related;
using UnityEngine.UI;

namespace PirateJam.Inventory_System
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
        [SerializeField] private ItemTooltip itemTooltip;
        [SerializeField] private Image draggableItem;

        private ItemSlot draggedSlot;

        private void OnValidate()
        {
            if (itemTooltip == null)
                itemTooltip = FindObjectOfType<ItemTooltip>();
        }

        private void Awake()
        {
            statPanel.SetStats(Strength, Agility, Vitality);
            statPanel.UpdateStatValues();

            // Setup Events:
            // Right click
            inventory.OnRightClickEvent += Equip;
            equipmentPanel.OnRightClickEvent += Unequip;

            // Pointer enter
            inventory.OnPointerEnterEvent += ShowTooltip;
            equipmentPanel.OnPointerEnterEvent += ShowTooltip;

            // Pointer exit
            inventory.OnPointerExitEvent += HideTooltip;
            equipmentPanel.OnPointerExitEvent += HideTooltip;

            // Begin drag
            inventory.OnBeginDragEvent += BeginDrag;
            equipmentPanel.OnBeginDragEvent += BeginDrag;

            // End drag
            inventory.OnEndDragEvent += EndDrag;
            equipmentPanel.OnEndDragEvent += EndDrag;

            // Drag
            inventory.OnDragEvent += Drag;
            equipmentPanel.OnDragEvent += Drag;

            // Drop
            inventory.OnDropEvent += Drop;
            equipmentPanel.OnDropEvent += Drop;
        }

        private void Equip(ItemSlot itemSlot)
        {
            EquipableItem equipableItem = itemSlot.Item as EquipableItem;
            if (equipableItem != null)
            {
                Equip(equipableItem);
            }
        }

        private void Unequip(ItemSlot itemSlot)
        {
            EquipableItem equipableItem = itemSlot.Item as EquipableItem;
            if (equipableItem != null)
            {
                Unequip(equipableItem);
            }
        }

        private void ShowTooltip(ItemSlot itemSlot)
        {
            EquipableItem equipableItem = itemSlot.Item as EquipableItem;
            if (equipableItem != null)
            {
                itemTooltip.ShowToolTip(equipableItem);
            }
        }

        private void HideTooltip(ItemSlot itemSlot)
        {
            itemTooltip.HideToolTip();
        }

        private void BeginDrag(ItemSlot itemSlot)
        {
            if (itemSlot.Item != null)
            {
                draggedSlot = itemSlot;
                draggableItem.sprite = itemSlot.Item.Icon;
                draggableItem.transform.position = Input.mousePosition;
                draggableItem.enabled = true;
            }
        }

        private void EndDrag(ItemSlot itemSlot)
        {
            draggedSlot = null;
            draggableItem.enabled = false;
        }

        private void Drag(ItemSlot itemSlot)
        {
            if (draggableItem.enabled)
                draggableItem.transform.position = Input.mousePosition;
        }

        private void Drop(ItemSlot dropItemSlot)
        {
            if (dropItemSlot == null) return;

            if (dropItemSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropItemSlot.Item))
            {
                EquipableItem dragItem = draggedSlot.Item as EquipableItem;
                EquipableItem dropItem = dropItemSlot.Item as EquipableItem;

                if (draggedSlot is EquipmentSlot)
                {
                    if (dragItem != null) dragItem.Unequip(this);
                    if (dropItem != null) dropItem.Equip(this);
                }
                if (dropItemSlot is EquipmentSlot)
                {
                    if (dragItem != null) dragItem.Equip(this);
                    if (dropItem != null) dropItem.Unequip(this);
                }
                statPanel.UpdateStatValues();

                Item draggedItem = draggedSlot.Item;
                draggedSlot.Item = dropItemSlot.Item;
                dropItemSlot.Item = draggedItem;
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