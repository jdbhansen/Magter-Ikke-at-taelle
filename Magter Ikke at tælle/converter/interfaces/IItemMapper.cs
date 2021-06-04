using System.Collections.Generic;

namespace Magter_Ikke_at_tælle.converter.interfaces
{
    interface IItemMapper
    {
        bool AddItem(IItem item);
        List<IItem> GetItems();
        void Clear();
    }
}
