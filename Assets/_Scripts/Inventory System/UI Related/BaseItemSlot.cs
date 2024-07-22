using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PirateJam.Inventory_System.UI_Related
{
    public class BaseItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected Image image;
        [SerializeField] protected TMP_Text amountText;

        public event Action<BaseItemSlot> OnPointerEnterEvent;
        public event Action<BaseItemSlot> OnPointerExitEvent;
        public event Action<BaseItemSlot> OnRightClickEvent;

        protected Color normalColor = Color.white;
        protected Color disabledColor = new(1, 1, 1, 0);

        protected Item _item;
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;

                if (_item == null)
                {
                    image.color = disabledColor;
                }
                else
                {
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
            return false;
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
    }
}