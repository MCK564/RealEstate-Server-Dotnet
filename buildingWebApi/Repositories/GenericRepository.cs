using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using buildingWebApi.Data;

namespace buildingWebApi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll(){
            return _dbSet.ToList();
        }

        public T GetById(long id){
            return _dbSet.Find(id);
        }

        public void Add(T entity){
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity){
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(long id){
           var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public void SaveChanges(){
            _context.SaveChanges();
        }
    }
}