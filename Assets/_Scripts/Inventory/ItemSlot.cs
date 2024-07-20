using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PirateJam.Inventory
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField] private Image image;

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

        protected virtual void OnValidate()
        {
            if (image == null)
                image = GetComponent<Image>();
        }
    }
}