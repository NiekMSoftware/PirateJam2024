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
        public string ItemDescription;

        [Space]
        [Range(1, 100)]
        public int MaximumStacks = 1;

        private void OnValidate()
        {
            string path = AssetDatabase.GetAssetPath(this);
            id = AssetDatabase.AssetPathToGUID(path);
        }

        public virtual Item GetCopy()
        {
            return this;
        }

        public virtual void Destroy()
        {

        }
    }
}