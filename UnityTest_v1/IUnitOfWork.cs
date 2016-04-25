using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityTest_v1.Models; 

namespace UnityTest_v1
{
    public interface IUnitOfWork : IDisposable
    {

        void Dispose();
        void Dispose(bool disposing);
        bool Save();
        DbContextEntities getDbContext(); 
        
        
    }
}
