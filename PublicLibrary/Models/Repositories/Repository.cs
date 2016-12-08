using PublicLibrary.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PublicLibrary.Models.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private DbContext db =new ApplicationDbContext();

        public Repository(DbContext context)
        {
            db = context;
        }

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public TEntity Get(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}