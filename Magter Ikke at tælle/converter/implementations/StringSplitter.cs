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
            return str.Split(newLine.ToCharArray());
        }
    }
}
