using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnityTest_v1.Models;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace UnityTest_v1.Tests
{
    
    [TestClass]
    public class TransactionRepositoryTest
    {

        private TransactionRepository transactionRepository;
        private Mock<DbContextEntities> moqDbContext;
        private Mock<DbSet<Transactions>> moqDbSetTransaction;
        private Mock<DbSet<Compte>> moqDbSetCompte;
        private List<Compte> comptes;
        private List<Transactions> transactions;

        
        [TestInitialize]
        public void initialize_test_methode()
        {
            // peupler la list des comptes 
            comptes = new List<Compte>(){
            
                new Compte { id= 1, 
                             idUtilisateur = 1, 
                             solde = 1000 , 
                             designation = "compte bna",
                             descript = "compte banquaire"},
                new Compte{
                             id = 2,
                             idUtilisateur = 1,
                             solde = 2000,
                             designation = "compte ccp" ,
                             descript = "compte postal"},
                new Compte{
                             id = 3,
                             idUtilisateur = 1,
                             solde = 3000,
                             designation = "compte espece" ,
                             descript = "argent de poche"
                                                        },
                new Compte{
                             id = 4,
                             idUtilisateur = 1,
                             solde = 4000,
                             designation = "compte paypal" ,
                             descript = "compte internet"
                                                        }
                };

            transactions = new List<Transactions>()
            {
               new Transactions(){
                               
                              id= 1,
                              idCategorie= 1,
                              idCompte = 1,
                              montant = 100,
                              typeTransact = "DEPENSE",
                              dateCreation = new DateTime(),
                              designation = "une transaction"
                                            },

               new Transactions(){
                               
                              id= 5,
                              idCategorie= 1,
                              idCompte = 2,
                              montant = 500,
                              typeTransact = "entree",
                              dateCreation = new DateTime(),
                              designation = "une transaction"
                                            }
            };


        }

        [TestMethod]
        public void get_all_test ()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTransaction = new Mock<DbSet<Transactions>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbSetTransaction.As<IQueryable<Transactions>>().Setup(m => m.Provider).Returns(transactions.AsQueryable().Provider);
            moqDbSetTransaction.As<IQueryable<Transactions>>().Setup(m => m.Expression).Returns(transactions.AsQueryable().Expression);
            moqDbSetTransaction.As<IQueryable<Transactions>>().Setup(m => m.ElementType).Returns(transactions.AsQueryable().ElementType);
            moqDbSetTransaction.As<IQueryable<Transactions>>().Setup(m => m.GetEnumerator()).Returns(transactions.AsQueryable().GetEnumerator());

            transactionRepository = new TransactionRepository(moqDbContext.Object, moqDbSetTransaction.Object, moqDbSetCompte.Object);
            IEnumerable<Transactions> resultat = transactionRepository.GetAll();

            Assert.IsTrue(resultat.Count() == 2);
        }

        [TestMethod]
        public void get_by_id_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTransaction = new Mock<DbSet<Transactions>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbSetTransaction.As<IQueryable<Transactions>>().Setup(m => m.Provider).Returns(transactions.AsQueryable().Provider);
            moqDbSetTransaction.As<IQueryable<Transactions>>().Setup(m => m.Expression).Returns(transactions.AsQueryable().Expression);
            moqDbSetTransaction.As<IQueryable<Transactions>>().Setup(m => m.ElementType).Returns(transactions.AsQueryable().ElementType);
            moqDbSetTransaction.As<IQueryable<Transactions>>().Setup(m => m.GetEnumerator()).Returns(transactions.AsQueryable().GetEnumerator());

            transactionRepository = new TransactionRepository(moqDbContext.Object, moqDbSetTransaction.Object, moqDbSetCompte.Object);
            Transactions resultat = transactionRepository.GetByID(1);

            Assert.IsTrue(resultat.id == transactions.ElementAt(0).id);
        }

        [TestMethod]
        public void insert_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTransaction = new Mock<DbSet<Transactions>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(2); // le resultat attendu
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));

            transactionRepository = new TransactionRepository(moqDbContext.Object, moqDbSetTransaction.Object, moqDbSetCompte.Object);

            bool resultatI = transactionRepository.Insert(new Transactions
            {
                id = 2,
                idCategorie = 1,
                idCompte = 2,
                montant = 200,
                designation = "une transaction",
                dateCreation = new DateTime(),
                typeTransact = "depense"
            });

            bool resultatII = transactionRepository.Insert(new Transactions
            {
                id = 3,
                idCategorie = 1,
                idCompte = 3,
                montant = 200,
                designation = "une transaction",
                dateCreation = new DateTime(),
                typeTransact = "entree"
            });


            Assert.IsTrue(resultatII);
            Assert.IsTrue(resultatI);
            Assert.IsTrue(comptes.ElementAt(1).solde == 1800);
            Assert.IsTrue(comptes.ElementAt(2).solde == 3200);


        }

        [TestMethod]
        public void delete_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTransaction = new Mock<DbSet<Transactions>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(2); // le resultat attendu
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTransaction.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transactions.FirstOrDefault(transaction => transaction.id == (int)ids[0]));
            moqDbSetTransaction.Setup(m => m.Remove(It.IsAny<Transactions>())).Returns((Transactions t) => { transactions.Remove(t); return t; });

            transactionRepository = new TransactionRepository(moqDbContext.Object, moqDbSetTransaction.Object, moqDbSetCompte.Object);

            bool resultatI = transactionRepository.Delete(1);
            bool resultatII = transactionRepository.Delete(5);

            Assert.IsTrue(resultatI);
            Assert.IsTrue(comptes.ElementAt(0).solde == 1100);

            Assert.IsTrue(resultatII);
            Assert.IsTrue(comptes.ElementAt(1).solde == 1500);

        }

        [TestMethod]
        public void update_montant_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTransaction = new Mock<DbSet<Transactions>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(2); // le resultat attendu
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTransaction.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transactions.FirstOrDefault(transaction => transaction.id == (int)ids[0]));

            transactionRepository = new TransactionRepository(moqDbContext.Object, moqDbSetTransaction.Object, moqDbSetCompte.Object);

            bool resultatI = transactionRepository.Update(new Transactions
            {
                id = 1,
                idCompte = 1,
                montant = 200,
                typeTransact="depense"

            });

            bool resultatII = transactionRepository.Update(new Transactions
            {
                id = 5,
                idCompte = 2,
                montant = 100,
                typeTransact="entree"

            });

            Assert.IsTrue(resultatI);
            Assert.IsTrue(comptes.ElementAt(0).solde == 900);

            Assert.IsTrue(resultatII);
            Assert.IsTrue(comptes.ElementAt(1).solde == 1600);


        }

        [TestMethod]
        public void update_typeTransact_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTransaction = new Mock<DbSet<Transactions>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(2); // le resultat attendu
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTransaction.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transactions.FirstOrDefault(transaction => transaction.id == (int)ids[0]));

            transactionRepository = new TransactionRepository(moqDbContext.Object, moqDbSetTransaction.Object, moqDbSetCompte.Object);

            bool resultatI = transactionRepository.Update(new Transactions
            {
                id = 1,
                idCompte = 1,
                montant = 100,
                typeTransact = "entree"

            });

            bool resultatII = transactionRepository.Update(new Transactions
            {
                id = 5,
                idCompte = 2,
                montant = 500,
                typeTransact = "depense"

            });

            Assert.IsTrue(resultatI);
            Assert.IsTrue(comptes.ElementAt(0).solde == 1200);

            Assert.IsTrue(resultatII);
            Assert.IsTrue(comptes.ElementAt(1).solde == 1000);


        }

        [TestMethod]
        public void update_compte_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTransaction = new Mock<DbSet<Transactions>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(3); // le resultat attendu
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTransaction.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transactions.FirstOrDefault(transaction => transaction.id == (int)ids[0]));

            transactionRepository = new TransactionRepository(moqDbContext.Object, moqDbSetTransaction.Object, moqDbSetCompte.Object);

            bool resultatI = transactionRepository.Update(new Transactions
            {
                id = 1,
                idCompte = 3,
                montant = 100,
                typeTransact = "depense"

            });

            bool resultatII = transactionRepository.Update(new Transactions
            {
                id = 5,
                idCompte = 4,
                montant = 500,
                typeTransact = "entree"

            });

            Assert.IsTrue(resultatI);
            Assert.IsTrue(comptes.ElementAt(0).solde == 1100);// ancien compte
            Assert.IsTrue(comptes.ElementAt(2).solde == 2900);// nouveau compte

            Assert.IsTrue(resultatII);
            Assert.IsTrue(comptes.ElementAt(1).solde == 1500);//ancien compte
            Assert.IsTrue(comptes.ElementAt(3).solde == 4500);//nouveau compte


        }


        [TestMethod]
        public void update_compte_typeTransact_montant_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTransaction = new Mock<DbSet<Transactions>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(3); // le resultat attendu
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTransaction.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transactions.FirstOrDefault(transaction => transaction.id == (int)ids[0]));

            transactionRepository = new TransactionRepository(moqDbContext.Object, moqDbSetTransaction.Object, moqDbSetCompte.Object);

            bool resultatI = transactionRepository.Update(new Transactions
            {
                id = 1,
                idCompte = 3,
                montant = 200,
                typeTransact = "entree"

            });

            bool resultatII = transactionRepository.Update(new Transactions
            {
                id = 5,
                idCompte = 4,
                montant = 600,
                typeTransact = "depense"

            });

            Assert.IsTrue(resultatI);
            Assert.IsTrue(comptes.ElementAt(0).solde == 1100);// ancien compte
            Assert.IsTrue(comptes.ElementAt(2).solde == 3200);// nouveau compte

            Assert.IsTrue(resultatII);
            Assert.IsTrue(comptes.ElementAt(1).solde == 1500);//ancien compte
            Assert.IsTrue(comptes.ElementAt(3).solde == 3400);//nouveau compte


        }
    }
}
