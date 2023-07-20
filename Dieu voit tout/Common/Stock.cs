using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dieu_voit_tout.Common
{
    public class Stock
    {
        public long Id { get; set; }
        public enum Operation { In=0, Out=1}
        public long ArticleId { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;



    }
}
