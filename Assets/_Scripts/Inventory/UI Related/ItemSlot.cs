using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PirateJam.Inventory.UI_Related
{
    public class ItemSlot : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image image;

        public event Action<Item> OnRightClickEvent;

        private Item item;
        public Item Item
        {
            get { return item; }
            set {
                item = value;

                if (item == null) {
                    image.enabled = false;
                }
                else {
                    image.sprite = item.Icon;
                    image.enabled = true;
                }
            }
        }

        void Start()
        {
            if (FindObjectOfType<EventSystem>() == null)
            {
                GameObject eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
            {
                if (Item != null && OnRightClickEvent != null)
                {
                    OnRightClickEvent(Item);
                }
                else
                {
                    if (Item == null)
                        Debug.LogError("Item is null.");
                    if (OnRightClickEvent == null)
                        Debug.LogError("OnRightClickEvent is null.");
                }
            }
        }

        protected virtual void OnValidate()
        {
            if (image == null)
                image = GetComponent<Image>();
        }
    }
}