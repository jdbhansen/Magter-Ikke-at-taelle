using Magter_Ikke_at_tælle.converter.interfaces;
using System;

namespace Magter_Ikke_at_tælle.converter.implementations
{
    public class Item : IItem
    {
        private ItemCategory _itemType;
        public int Id { get; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public ItemCategory ItemType => _itemType;

        public Item(int id, int qty)
        {
            Id = id;
            Quantity = qty;
            SetItemType();
        }
        public Item(int id, int qty, string name)
        {
            Id = id;
            Quantity = qty;
            Name = name;
            SetItemType();
        }
        public int AddToQuantity(int quantityToAdd)
        {
            Quantity += quantityToAdd;
            return Quantity;
        }

        public override string ToString()
        {
            string str = "" + Id + "\t" + Quantity + "\t" + ItemType;
            if (Name != null && Name.Length > 0)
            {
                str += ", " + Name;
            }
            str += "\n";
            return str;
        }

        private void SetItemType()
        {
            if (Id > 60000 && Id < 61000)
            {
                _itemType = ItemCategory.Accessory;
                return;
            }
            if (Id > 61000 && Id < 62000)
            {
                _itemType = ItemCategory.Headset;
                return;
            }
            if (Id > 62000 && Id < 63000)
            {
                _itemType = ItemCategory.Mouse;
                return;
            }
            if (Id > 63000 && Id < 64000)
            {
                _itemType = ItemCategory.Keyboard;
                return;
            }
            if (Id > 64000 && Id < 65000)
            {
                _itemType = ItemCategory.Mousepad;
                return;
            }
            _itemType = ItemCategory.Unknown;
        }

        public void SetItemCategory(ItemCategory itemType)
        {
            if (((int)itemType) > 0 && ((int)itemType) < Enum.GetNames(typeof(ItemCategory)).Length)
            {
                _itemType = itemType;
                return;
            }
            _itemType = ItemCategory.Unknown;
        }
    }
}
