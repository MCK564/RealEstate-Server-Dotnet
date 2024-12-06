using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Services.UserS
{
    public interface IUserService
    {
        
        IEnumerable<UserEntity> GetAll();
        UserEntity GetById(long id);
        void Create(UserEntity user);
        void Update(UserEntity user);
        void Delete(long id);
    }
}