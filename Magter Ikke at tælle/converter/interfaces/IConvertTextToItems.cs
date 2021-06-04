using System.Collections.Generic;

namespace Magter_Ikke_at_tælle.converter.interfaces
{

    public interface IConvertTextToItems
    {
        //        string[] SplitStringIntoLines(string str);
        List<IItem> ConvertText(string str);
        int CountOfOrderLines();
        void ClearItems();
        void ResetCount();
    }
}
