using UnityEditor;
using UnityEngine;

namespace PirateJam.Inventory_System
{
    [CreateAssetMenu]
    public class Item : ScriptableObject
    {
        [SerializeField] private string id;
        public string ID { get { return id; } }
        
        [Space]
        public Sprite Icon;
        public string ItemName;

        private void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            id = AssetDatabase.AssetPathToGUID(path);
        }
    }
}