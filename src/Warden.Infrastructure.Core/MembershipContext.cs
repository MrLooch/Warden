using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Principal;
using Warden.DataModel.Entities;

namespace Warden.Infrastructure.Core
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public UserEntity User { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}
