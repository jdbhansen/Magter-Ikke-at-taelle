using Magter_Ikke_at_tælle.converter.interfaces;

namespace Magter_Ikke_at_tælle.converter.implementations
{
    public class Item : IItem
    {
        public int Id { get; }
        public int Quantity { get; set; }
        public string Name { get; }
        public Item(int id, int qty)
        {
            Id = id;
            Quantity = qty;
        }
        public Item(int id, int qty, string name)
        {
            Id = id;
            Quantity = qty;
            Name = name;
        }


        public int AddToQuantity(int quantityToAdd)
        {
            Quantity += quantityToAdd;
            return Quantity;
        }

        public override string ToString()
        {
            string str = "" + Id + "\t" + Quantity;
            if (Name != null && Name.Length != 0)
            {
                str += "\t" + Name + "\n";
            }
            else
            {
                str += "\n";
            }
            return str;
        }
    }
}
