using Magter_Ikke_at_tælle.converter.interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Magter_Ikke_at_tælle.converter.implementations
{
    public class ItemMapper : IItemMapper
    {
        private Dictionary<int, IItem> itemMap;

        public ItemMapper()
        {
            itemMap = new Dictionary<int, IItem>();
        }

        public bool AddItem(IItem item)
        {
            if (item != null && item.Id != 0)
            {
                if (itemMap.ContainsKey(item.Id))
                {
                    IItem mappedItem = itemMap[item.Id];
                    mappedItem.AddToQuantity(item.Quantity);
                    itemMap[mappedItem.Id] = mappedItem;
                    return true;
                }
                else
                {
                    itemMap.Add(item.Id, item);
                    return true;
                }
            }
            return false;
        }

        public void Clear()
        {
            itemMap.Clear();
        }

        public List<IItem> GetItems()
        {
            if (itemMap.Count == 0)
            {
                return null;
            }

            List<IItem> itemList = (from KeyValuePair<int, IItem>
                                    item in itemMap
                                    select item.Value)
                                    .ToList();
            itemList.Sort((x, y) => x.Id.CompareTo(y.Id));
            return itemList;
        }
    }
}
