using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Services.UserS
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<UserEntity> _userRepository;

        public UserService(IGenericRepository<UserEntity> repository)
        {
            _userRepository = repository;
        }

        public IEnumerable<UserEntity> GetAll(){
            return _userRepository.GetAll();
        }

       


    }
}