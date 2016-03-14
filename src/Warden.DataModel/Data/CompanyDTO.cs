using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.DataModel.Entities;

namespace Warden.DataModel
{
    public class Company : EntityBase
    {
        public string Name { get; set; }

        public IEnumerable<Site> Sites { get; set; }
    }
}
