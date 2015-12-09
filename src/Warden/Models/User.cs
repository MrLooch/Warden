using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warden.Models
{
    public class User
    {
        public string Name { get; set; }
        public Role Role { get; set; }
        public Company Company { get; set; }
        public Site Site { get; set; }
    }
}
