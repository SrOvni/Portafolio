using UnityEngine;

namespace RG.Systems
{
    public abstract class ItemBase : ScriptableObject
    {
        public string ItemName;
        public Sprite Icon;
        public bool IsStackable;
        public int MaxStackSize = 1;
    }
    public class InventorySlot
    {
        public ItemBase Item { get; private set; }
        public int Quantity { get; private set; }
        public bool IsEmpty => Item == null;
    }
}
