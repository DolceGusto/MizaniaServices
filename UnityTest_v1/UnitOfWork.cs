using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityTest_v1.Models;

namespace UnityTest_v1
{
    public class UnitOfWork : IUnitOfWork
    {

        public DbContextEntities _context;
        public UnitOfWork(DbContextEntities context)
        {
            this._context = new DbContextEntities();
        }

        public DbContextEntities getDbContext() 
        {
            return _context; 
        }

        public bool Save()
        {
            if (_context.SaveChanges() == 1) return true;
            else return false; 
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}