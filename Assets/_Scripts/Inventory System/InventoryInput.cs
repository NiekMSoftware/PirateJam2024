using UnityEngine;

namespace PirateJam.Inventory_System
{
    public class InventoryInput : MonoBehaviour
    {
        [SerializeField] private GameObject characterPanelGameObject;
        [SerializeField] private GameObject equipmentPanelGameObject;
        [SerializeField] private KeyCode[] toggleInventoryKeys;
        [SerializeField] private KeyCode[] toggleCharacterPanelKeys;

        private void Update()
        {
            for (int i = 0; i < toggleCharacterPanelKeys.Length; i++)
            {
                if (Input.GetKeyDown(toggleCharacterPanelKeys[i]))
                {
                    characterPanelGameObject.SetActive(!characterPanelGameObject.activeSelf);

                    if (characterPanelGameObject.activeSelf)
                    {
                        equipmentPanelGameObject.SetActive(true);
                        ShowMouseCursor();
                    }
                    else
                    {
                        HideMouseCursor();
                    }

                    break;
                }
            }

            for (int i = 0; i < toggleInventoryKeys.Length; i++)
            {
                if (Input.GetKeyDown(toggleInventoryKeys[i]))
                {
                    if (!characterPanelGameObject.activeSelf)
                    {
                        characterPanelGameObject.SetActive(true);
                        equipmentPanelGameObject.SetActive(true);
                        ShowMouseCursor();
                    }
                    else
                    {
                        characterPanelGameObject.SetActive(false);
                        HideMouseCursor();
                    }
                    break;
                }
            }
        }

        public void ShowMouseCursor()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void HideMouseCursor() 
        { 
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void ToggleEquipmentPannel()
        {
            equipmentPanelGameObject.SetActive(!equipmentPanelGameObject.activeSelf);
        }
    }
}