using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warden.Models
{
    public class Company
    {
        public string Name { get; set; }

        public IEnumerable<Site> Sites { get; set; }
    }
}
