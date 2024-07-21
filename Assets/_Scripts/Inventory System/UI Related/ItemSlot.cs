using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;

namespace PirateJam.Inventory_System.UI_Related
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [SerializeField] private Image image;
        [SerializeField] TMP_Text amountText;

        public event Action<ItemSlot> OnPointerEnterEvent;
        public event Action<ItemSlot> OnPointerExitEvent;
        public event Action<ItemSlot> OnRightClickEvent;
        public event Action<ItemSlot> OnBeginDragEvent;
        public event Action<ItemSlot> OnEndDragEvent;
        public event Action<ItemSlot> OnDragEvent;
        public event Action<ItemSlot> OnDropEvent;

        private Color normalColor = Color.white;
        private Color disabledColor = new(1, 1, 1, 0);

        private Item _item;
        public Item Item
        {
            get { return _item; }
            set {
                _item = value;

                if (_item == null) {
                    image.color = disabledColor;
                }
                else {
                    image.sprite = _item.Icon;
                    image.color = normalColor;
                }
            }
        }

        private int _amount;
        public int Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                amountText.enabled = _item != null && _item.MaximumStacks > 1 && _amount > 0;
                if (amountText.enabled)
                {
                    amountText.text = _amount.ToString();
                }
            }
        }

        protected virtual void OnValidate()
        {
            if (image == null)
                image = GetComponent<Image>();

            if (amountText == null)
                amountText = GetComponentInChildren<TMP_Text>();
        }

        public virtual bool CanReceiveItem(Item item)
        {
            return true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                if (OnRightClickEvent != null)
                    OnRightClickEvent(this);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (OnPointerEnterEvent != null) 
                OnPointerEnterEvent(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        { 
            if (OnPointerExitEvent != null)
                OnPointerExitEvent(this);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (OnBeginDragEvent != null)
                OnBeginDragEvent(this);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (OnEndDragEvent != null)
                OnEndDragEvent(this);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (OnDragEvent != null)
                OnDragEvent(this);
        }

        void IDropHandler.OnDrop(PointerEventData eventData)
        {
            if (OnDropEvent != null)
                OnDropEvent(this);
        }
    }
}