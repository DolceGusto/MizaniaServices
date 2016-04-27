using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityTest_v1.IServices;
using UnityTest_v1.Models;
using UnityTest_v1.Services; 

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
            Container.RegisterType<IGenericRepository<Utilisateur>, GenericRepository<Utilisateur>>();
            Container.RegisterType<IGenericRepository<Compte>, GenericRepository<Compte>>();
            Container.RegisterType<IGenericRepository<Categorie>, GenericRepository<Categorie>>();
            Container.RegisterType<IUserService, UserService>();
            Container.RegisterType<ICompteService, CompteService>();

            Container.RegisterType<ICategorieService, CategorieService>();
            Container.RegisterType<IGenericRepository<Transactions>, TransactionRepository>();
            Container.RegisterType<IGenericRepository<Transfert>, TransfertRepository>();

            Container.RegisterType<ITransfertService, TransfertService>();

            Container.RegisterType<ITransactionService, TransactionsService>();

        }

        public static IUnityContainer Container { get; private set; }

    
    }
}