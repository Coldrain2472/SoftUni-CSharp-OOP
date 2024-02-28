using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony.Models.Interfaces
{
    public interface IBrowsable : ICallable
    {
        string Website { get; }
        void Browse(string website);
    }
}
