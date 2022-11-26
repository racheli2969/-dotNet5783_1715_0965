using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlApi
{
    public interface IBl
    {
        public ICart cart { get;  }
        public IOrder order { get; }    
        public IProduct product { get; }
    }
}
