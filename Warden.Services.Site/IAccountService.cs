using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Authentication;
using Warden.DataModel.Authentication;
using Warden.DataModel.Entities;

namespace Warden.Server.Services
{
    public interface IAccountService
    {
        UserEntity registereUser(UserRegistrationDTO registration);
        Task<UserEntity> login(UserLoginDTO user, AuthenticationManager authManager);
        Task<IEnumerable<UserEntity>> FindByUserName(string name);
        Task<IEnumerable<UserEntity>> FindByEmail(string email);
        Task<IEnumerable<UserEntity>> Get();
        Task<IEnumerable<UserEntity>> GetById(Guid id);
        void Add(UserEntity site);
        void Update(UserEntity site);
        void Delete(UserEntity user);
    }
}
