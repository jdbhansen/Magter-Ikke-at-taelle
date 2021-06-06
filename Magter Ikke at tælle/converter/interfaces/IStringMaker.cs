using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magter_Ikke_at_tælle.converter.interfaces
{
    public interface IStringMaker
    {
        string ReformatStringsToLines(string str);
        string ConvertItemsToString(List<IItem> list);
        void Clear();
    }
}
