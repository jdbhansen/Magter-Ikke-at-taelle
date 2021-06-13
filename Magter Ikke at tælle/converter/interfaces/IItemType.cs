using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magter_Ikke_at_tælle.converter.interfaces
{
    public interface IItemType
    {
        string Name { get; }
        int MinimumId { get; }
        int MaximumId { get; }
        int[] GetIdRange();
        void SetIdRange(int[] idRange);
    }
}
