using UnityEngine;

namespace PirateJam.Inventory_System
{
    public class ItemChest : MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private Inventory inventory;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private Color emptyColor;
        [SerializeField] private KeyCode itemPickupKeycode = KeyCode.E;

        private bool isInRange;
        private bool isEmpty;

        private void OnValidate()
        {
            if (inventory == null)
                inventory = FindObjectOfType<Inventory>();

            spriteRenderer.sprite = item.Icon;
            spriteRenderer.enabled = false;
        }

        private void Update()
        {
            if (isInRange && Input.GetKeyDown(itemPickupKeycode))
            {
                if (!isEmpty)
                {
                    inventory.AddItem(item);
                    isEmpty = true;
                    spriteRenderer.color = emptyColor;
                }
            }
        }

        private void CheckCollision(GameObject gameObject, bool state)
        {
            if (playerMask == (playerMask | (1 << gameObject.layer)))
            {
                isInRange = state;
                spriteRenderer.enabled = state;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            CheckCollision(other.gameObject, state: true);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            CheckCollision(other.gameObject, state: false); 
        }
    }
}