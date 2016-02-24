using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warden.DataModel.Authentication;

namespace Warden.Server.Services
{
    public interface IAccountService
    {
        Task<IEnumerable<UserRegistration>> FindByUserName(string name);
        Task<List<UserRegistration>> Get();
        Task<UserRegistration> GetById(Guid id);
        Task Add(UserRegistration site);
        Task Update(UserRegistration site);
        Task Delete(Guid id);
    }
}
