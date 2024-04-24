using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.Console.Dtos.User
{
    public class LocalDB
    {
        public int uID { get; set; }
        public string userName { get; set; }
        public string token { get; set; }

        public int groupId { get; set; }
        
    }
}
