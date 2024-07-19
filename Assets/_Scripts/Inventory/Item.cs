using UnityEngine;

namespace PirateJam.Inventory
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        public Sprite Icon;
        public string ItemName;
    }
}