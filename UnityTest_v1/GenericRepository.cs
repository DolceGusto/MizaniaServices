using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Linq.Expressions; 
using UnityTest_v1.Models; 


namespace UnityTest_v1
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
       
        private  DbContextEntities _context;
        private DbSet<TEntity> _DbSet;
   
        public GenericRepository()
        {   
            this._context = new DbContextEntities();
            this._DbSet = _context.Set<TEntity>();
        }

        public GenericRepository(DbContextEntities dbContext, DbSet<TEntity> dbSet)
        {
            this._context = dbContext;
            this._DbSet = dbSet;
        }

        public IEnumerable<TEntity> GetAll()
        {
            
            IQueryable<TEntity> query = _DbSet;
            return query.ToList();
        }

        public TEntity GetByID(object id)
        {
            return _DbSet.Find(id);
        }

        public bool Insert(TEntity entity)
        {
            _DbSet.Add(entity);
            if (_context.SaveChanges() == 1)
                return true;
            else return false;

        }

        public bool Delete(object id)
        {
            
            TEntity entityToDelete = _DbSet.Find(id);
            Delete(entityToDelete);
            if (_context.SaveChanges() == 1)
                return true;
            else return false;
        }
       
        public void Delete(TEntity entityToDelete)
        {
            
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _DbSet.Attach(entityToDelete);
            }
            _DbSet.Remove(entityToDelete);
        }

        public bool Update(TEntity entityToUpdate)
        {
            _DbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
            if (_context.SaveChanges() == 1)
                return true;
            else return false;
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = _DbSet.Where(predicate);
            return query.ToList();
        }
    }
}