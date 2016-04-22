using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityTest_v1.Models; 

namespace UnityTest_v1
{
    public class IoC_Config
    {
        static IoC_Config()
        {
            Configure();
        }

        public static void Configure()
        {
            Container = new UnityContainer();
          //  Container.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(),
         //   new InjectionConstructor(new DbContextEntities()));
            Container.RegisterType<IGenericRepository<Utilisateur>, GenericRepository<Utilisateur>>();
            Container.RegisterType<IGenericRepository<Compte>, GenericRepository<Compte>>();
            Container.RegisterType<IUserService, UserService>();
            Container.RegisterType<ICompteService, CompteService>();
        }

        public static IUnityContainer Container { get; private set; }

    
    }
}