using Magter_Ikke_at_tælle.converter.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magter_Ikke_at_tælle.converter.implementations
{
    public class StringSplitter : IStringSplitter
    {
        private static string newLine;

        public StringSplitter()
        {
            newLine = Environment.NewLine;
        }
        public string[] GetLines(string str)
        {
            List<string> stringLines = new List<string>();
            string[] singleStrings = str.Split();
            int stringStart = 0;
            for (int i = 0; i < singleStrings.Length; i++)
            {
                if (singleStrings[i].Contains(newLine))
                {
                    for (int k = stringStart; k < i; k++)
                    {
                        stringLines.Add(singleStrings[k]);
                        stringStart = i;
                    }
                }
            }
            return stringLines.ToArray();
        }
    }
}
