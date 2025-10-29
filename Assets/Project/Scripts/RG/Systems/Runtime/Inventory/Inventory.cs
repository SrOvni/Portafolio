using System.Collections.Generic;
using UnityEngine;
namespace RG.Systems
{
    public abstract class Inventory : MonoBehaviour
    {
        public abstract List<InventorySlot> InventorySlots { get; set; }
    }
}
