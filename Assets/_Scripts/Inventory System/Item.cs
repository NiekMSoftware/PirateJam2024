using UnityEngine;

namespace PirateJam.Inventory_System
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public Sprite Icon;
        public string ItemName;
    }
}