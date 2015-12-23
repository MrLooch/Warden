using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.DataModel.Entities;

namespace Warden.DataModel
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public Company Company { get; set; }
        // Reference id instead of concert????                
        public Site Site { get; set; }
    }
}
