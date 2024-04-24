using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.Services.Helpers
{
    public interface IJwtHelper
    {
        public string GenerateToken(int userId);
    }
}
