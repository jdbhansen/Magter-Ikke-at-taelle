namespace Magter_Ikke_at_tælle.converter.interfaces
{
    public interface IItem
    {
        int Id { get; }
        string Name { get; set; }
        int Quantity { get; set; }
        ItemCategory ItemType { get; }
        void SetItemCategory(ItemCategory itemType);
        int AddToQuantity(int quantityToAdd);
    }
    public enum ItemCategory : int
    {
        Unknown,
        Accessory,
        Headset,
        Mouse,
        Keyboard,
        Mousepad
    }
}
