using System.Collections.Generic;

namespace Magter_Ikke_at_tælle.converter.interfaces
{

    public interface IConvertTextToItems
    {
        List<IItem> ConvertTextToItems(string str);
        List<IItem> ConvertTextToItemsFromTabbedStringLines(string input, int[] coords);
        int CountOfOrderLines();
        void ClearItems();
        void ResetCount();
    }
}
