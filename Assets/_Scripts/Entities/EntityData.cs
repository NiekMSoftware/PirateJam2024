using UnityEngine;

public class EntityData : ScriptableObject
{
    [field: Header("Entity Stats")]
    [field: SerializeField] public string EntityName { get; private set; }
    [field: SerializeField] public int Health { get; set; }
    [field: SerializeField] public int MaxHealth { get; private set; }

    [field: Space()]
    [field: SerializeField] public int Damage { get; private set;  }
}
