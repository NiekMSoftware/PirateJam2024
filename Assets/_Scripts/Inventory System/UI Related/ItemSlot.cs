using System;
using UnityEngine.EventSystems;
using UnityEngine;

namespace PirateJam.Inventory_System.UI_Related
{
    public class ItemSlot : BaseItemSlot, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        public event Action<BaseItemSlot> OnBeginDragEvent;
        public event Action<BaseItemSlot> OnEndDragEvent;
        public event Action<BaseItemSlot> OnDragEvent;
        public event Action<BaseItemSlot> OnDropEvent;

        private Color dragColor = new Color(1, 1, 1, 0.5f);

        public override bool CanReceiveItem(Item item)
        {
            return true;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (OnBeginDragEvent != null)
                OnBeginDragEvent(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (OnEndDragEvent != null)
                OnEndDragEvent(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (OnDragEvent != null)
                OnDragEvent(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (OnDropEvent != null)
                OnDropEvent(this);
        }
    }
}