using Magter_Ikke_at_tælle.converter.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magter_Ikke_at_tælle.converter.implementations
{
    public class StringMaker : IStringMaker
    {
        private static readonly StringBuilder SB = new StringBuilder();
        private static string newLine;

        public StringMaker()
        {
            newLine = Environment.NewLine;
        }

        public string ConvertItemsToString(List<IItem> list)
        {
            Clear();
            if (list != null && list.Count != 0)
            {
                string str = "id\tqty\n";
                _ = SB.Append(str);
                foreach (IItem item in list)
                {
                    _ = SB.Append(item.ToString());
                }
            }
            return SB.ToString();
        }

        public string ReformatStringsToLines(string str)
        {
            Clear();
            string[] lines = GetLines(str);
            for (int i = 0; i < lines.Length; i++)
            {
                _ = SB.Append(lines[i] + "\n");
            }
            return SB.ToString();
        }

        private string[] GetLines(string str)
        {
            return str.Split(newLine.ToCharArray());
        }
        public void Clear()
        {
            _ = SB.Clear();
        }
    }
}
