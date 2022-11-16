using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    internal interface IDal
    {
        public IItem Item { get; }
        public IOrder Order { get; }
        public IOrderItem OrderItem { get; }

    }
}
