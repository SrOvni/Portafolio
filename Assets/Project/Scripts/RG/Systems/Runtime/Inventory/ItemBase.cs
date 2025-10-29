using UnityEngine;
using System.Collections.Generic;

namespace RG.Systems
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public abstract class Item : ScriptableObject
    {
        public string ItemName;
        public Sprite Icon;
        public bool IsStackable;
        public int MaxStackSize = 1;
    }
    public class InvetoryContainer : MonoBehaviour
    {
        [SerializeField] private int capacity = 5;
        public List<InventorySlot> slots = new();
        // private delegate 
    }
}
