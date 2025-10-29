namespace RG.Systems
{
    public class InventorySlot
    {
        public Item Item { get; private set; }
        public int Quantity { get; private set; }
        public bool IsEmpty => Item == null;
    }
}
