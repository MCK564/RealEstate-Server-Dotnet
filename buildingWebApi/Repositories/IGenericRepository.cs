using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace buildingWebApi.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        void Add(T entity);
        void Update(T entity);
        void Delete(long id);
        void SaveChanges();

    }
}