namespace Magter_Ikke_at_tælle.converter.interfaces
{
    public interface IItem
    {
        int Id { get; }
        string Name { get; }
        int Quantity { get; set; }
        int AddToQuantity(int quantityToAdd);
    }
}
