using Magter_Ikke_at_tælle.converter.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magter_Ikke_at_tælle.converter.implementations
{
    /// <summary>
    /// Since Enums are constant, this might be needed instead.
    /// </summary>
    public class ItemType : IItemType
    {
        public string Name { get; }
        public int MinimumId { get; private set; }
        public int MaximumId { get; private set; }

        public ItemType(string name, int[] idRange)
        {
            Name = name;
            if (idRange != null && idRange.Length == 2)
            {
                MinimumId = idRange[0];
                MaximumId = idRange[1];
            }
        }

        public int[] GetIdRange()
        {
            return new int[] { MinimumId, MaximumId };
        }

        public void SetIdRange(int[] idRange)
        {
            if (idRange != null && idRange.Length == 2)
            {
                MinimumId = idRange[0];
                MaximumId = idRange[1];
            }
        }

        public override string ToString()
        {
            return "" + Name + ", " + MinimumId + ", " + MaximumId;
        }
    }
}
