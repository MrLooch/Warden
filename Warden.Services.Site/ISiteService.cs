using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.DataModel;

namespace Warden.Services
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public interface ISiteService
    {
        Task<List<Site>> Get();
        Task<Site> GetById(Guid id);
        Task Add(Site site);
        Task Update(Site site);
        Task Delete(Guid id);
    }
}
