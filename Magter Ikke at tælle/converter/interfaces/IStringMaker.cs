using System.Collections.Generic;

namespace Magter_Ikke_at_tælle.converter.interfaces
{
    public interface IStringMaker
    {
        string ReformatStringsToLines(string str);
        string ConvertItemsToString(List<IItem> list);
        void Clear();
    }
}
