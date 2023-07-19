using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Common
{
    public class Order
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long Amount { get; set; }
        public decimal Total { get; set; }
    }
}
