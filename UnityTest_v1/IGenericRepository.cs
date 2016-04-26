<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions; 

namespace UnityTest_v1
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(); 
        TEntity GetByID(object id);
        bool Insert(TEntity entity);
        bool Delete(object id);
        void Delete(TEntity entityToDelete);
        bool Update(TEntity entityToUpdate);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions; 

namespace UnityTest_v1
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll(); 
        TEntity GetByID(object id);
        bool Insert(TEntity entity);
        bool Delete(object id);
        void Delete(TEntity entityToDelete);
        bool Update(TEntity entityToUpdate);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}
>>>>>>> 5597514c2bf9093f555916b1ed1b3425ad0b830d
