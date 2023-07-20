using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dieu_voit_tout.Common
{
    public class Users
    {
        public long Id{ get; set; }
        public string Name{ get; set; }
        public string Password { get; set; }
        public enum Role { Admin=0, Simple }

    }
}
