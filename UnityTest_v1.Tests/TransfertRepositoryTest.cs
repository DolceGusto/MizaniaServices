using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnityTest_v1.Models;
using System.Data.Entity;
using System.Linq;

namespace UnityTest_v1.Tests
{
    
    [TestClass]
    public class TransfertRepositoryTest
    {
        private TransfertRepository transfertRepository;
        private Mock<DbContextEntities> moqDbContext;
        private Mock<DbSet<Transfert>> moqDbSetTranfert ;
        private Mock<DbSet<Compte>> moqDbSetCompte;
        private List<Compte> comptes;
        private List<Transfert> transferts;


        [TestInitialize]
        public void InitializeTest()
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

                //peupler la liste des transferts 

                transferts = new List<Transfert>(){
                    new Transfert{
                        id = 1,
                        idCompteExpediteur = 1,
                        idCompteRecepteur = 2,
                        montant = 100,
                        dateCreation =new DateTime(),
                        designation = "tranfert 1"
                    }
                };


        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void get_all_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbSetTranfert.As<IQueryable<Transfert>>().Setup(m => m.Provider).Returns(transferts.AsQueryable().Provider);
            moqDbSetTranfert.As<IQueryable<Transfert>>().Setup(m => m.Expression).Returns(transferts.AsQueryable().Expression);
            moqDbSetTranfert.As<IQueryable<Transfert>>().Setup(m => m.ElementType).Returns(transferts.AsQueryable().ElementType);
            moqDbSetTranfert.As<IQueryable<Transfert>>().Setup(m => m.GetEnumerator()).Returns(transferts.AsQueryable().GetEnumerator());

            transfertRepository = new TransfertRepository(moqDbContext.Object, moqDbSetTranfert.Object, moqDbSetCompte.Object);
            IEnumerable<Transfert> resultat = transfertRepository.GetAll();

            Assert.IsTrue(resultat.Count() == 1);

        }

        [TestMethod]
        public void get_by_id_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbSetTranfert.As<IQueryable<Transfert>>().Setup(m => m.Provider).Returns(transferts.AsQueryable().Provider);
            moqDbSetTranfert.As<IQueryable<Transfert>>().Setup(m => m.Expression).Returns(transferts.AsQueryable().Expression);
            moqDbSetTranfert.As<IQueryable<Transfert>>().Setup(m => m.ElementType).Returns(transferts.AsQueryable().ElementType);
            moqDbSetTranfert.As<IQueryable<Transfert>>().Setup(m => m.GetEnumerator()).Returns( () => transferts.AsQueryable().GetEnumerator());

            transfertRepository = new TransfertRepository(moqDbContext.Object, moqDbSetTranfert.Object, moqDbSetCompte.Object);

            Transfert resultat = transfertRepository.GetByID(1);

            Assert.AreEqual(resultat.designation, transferts.ElementAt(0).designation);
            Assert.AreEqual(resultat.id, transferts.ElementAt(0).id);
        }

        [TestMethod]
        public void delete_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transferts.FirstOrDefault(transfert => transfert.id == (int)ids[0]));
            moqDbContext.Setup(m => m.SaveChanges() ).Returns(3);
            moqDbSetTranfert.Setup(m => m.Remove(It.IsAny<Transfert>())).Returns((Transfert t) => { transferts.Remove(t); return t; });

            transfertRepository = new TransfertRepository(moqDbContext.Object, moqDbSetTranfert.Object, moqDbSetCompte.Object);

            bool resulat = transfertRepository.Delete(1);

            Assert.IsTrue(resulat);
            Assert.IsTrue(comptes.ElementAt(0).solde == 1100);
            Assert.IsTrue(comptes.ElementAt(1).solde == 1900);


        }

        [TestMethod]
        public void insert_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(3); // le resultat attendu
            moqDbSetCompte.Setup(m => m.Find( It.IsAny<Object[]>() )  ).Returns(( Object[] ids ) => comptes.FirstOrDefault(compte => compte.id == (int) ids[0]));
            moqDbSetTranfert.Setup(m => m.Add( It.IsAny<Transfert>() ) ).Returns( (Transfert t) => { transferts.Add(t); return t; } );

            transfertRepository = new TransfertRepository(moqDbContext.Object, moqDbSetTranfert.Object, moqDbSetCompte.Object);

           bool resultat =  transfertRepository.Insert(new Transfert
            {
                id = 2,
                idCompteExpediteur = 1,
                idCompteRecepteur = 2,
                montant = 100,
                dateCreation = new DateTime(),
                designation = " tranfert 2"
            });

            // assertion sur la maj du compte expediteur et recepteur 

            Assert.IsTrue(comptes.ElementAt(0).solde == 900 );
            Assert.IsTrue(comptes.ElementAt(1).solde == 2100);
            Assert.IsTrue(transferts.ElementAt(1).id == 2);
            Assert.IsTrue(resultat);

            


        }

        [TestMethod]
        public void update_id_expediteur_seulment_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();
            
            moqDbContext.Setup(c => c.SaveChanges()).Returns(3); // changment dans l'expediteur seulement 
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transferts.FirstOrDefault(t => t.id == (int) ids[0]));
            moqDbSetTranfert.Setup(m => m.Add( It.IsAny<Transfert>() )).Returns(( Transfert t ) => { transferts.Add(t); return t; });

            transfertRepository = new TransfertRepository(moqDbContext.Object,moqDbSetTranfert.Object,moqDbSetCompte.Object);

            bool resualtat = transfertRepository.Update(new Transfert
            {
                id = 1,
                idCompteExpediteur = 3,
                idCompteRecepteur = 2,
                montant = 100,
                dateCreation = new DateTime(),
                designation = "transfert 1"

            });

            Assert.IsTrue(resualtat);// le update a resussie
            Assert.IsTrue(comptes.ElementAt(0).solde == 1100); //ancienne expediteur
            Assert.IsTrue(comptes.ElementAt(2).solde == 2900 );// nouveau expediteur
            Assert.IsTrue(comptes.ElementAt(1).solde == 2000 );// le recepteur

            
        }


        [TestMethod]
        public void update_id_recepteur_seulment_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(3); // changment dans le recepteur seulement 
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transferts.FirstOrDefault(t => t.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Add(It.IsAny<Transfert>())).Returns((Transfert t) => { transferts.Add(t); return t; });

            transfertRepository = new TransfertRepository(moqDbContext.Object, moqDbSetTranfert.Object, moqDbSetCompte.Object);

            bool resualtat = transfertRepository.Update(new Transfert
            {
                id = 1,
                idCompteExpediteur = 1,
                idCompteRecepteur = 3,
                montant = 100,
                dateCreation = new DateTime(),
                designation = "transfert 1"

            });

            Assert.IsTrue(resualtat);// le update a resussie
            Assert.IsTrue(comptes.ElementAt(0).solde == 1000); // expediteur
            Assert.IsTrue(comptes.ElementAt(1).solde == 1900);// l'ancien recepteur
            Assert.IsTrue(comptes.ElementAt(2).solde == 3100 ); // le nouveau recepteur
        }

        [TestMethod]
        public void update_montant_seulment_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(3); // changment dans le montant seulement 
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transferts.FirstOrDefault(t => t.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Add(It.IsAny<Transfert>())).Returns((Transfert t) => { transferts.Add(t); return t; });

            transfertRepository = new TransfertRepository(moqDbContext.Object, moqDbSetTranfert.Object, moqDbSetCompte.Object);

            bool resualtat = transfertRepository.Update(new Transfert
            {
                id = 1,
                idCompteExpediteur = 1,
                idCompteRecepteur = 2,
                montant = 200,
                dateCreation = new DateTime(),
                designation = "transfert 1"

            });

            Assert.IsTrue(resualtat);// le update a resussie
            Assert.IsTrue(comptes.ElementAt(0).solde == 900); // expediteur
            Assert.IsTrue(comptes.ElementAt(1).solde == 2100);// le recepteur

            
        }

        [TestMethod]
        public void update_montant_et_recepteur_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(3); // changment dans le montant et le recepteur
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transferts.FirstOrDefault(t => t.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Add(It.IsAny<Transfert>())).Returns((Transfert t) => { transferts.Add(t); return t; });

            transfertRepository = new TransfertRepository(moqDbContext.Object, moqDbSetTranfert.Object, moqDbSetCompte.Object);

            bool resualtat = transfertRepository.Update(new Transfert
            {
                id = 1,
                idCompteExpediteur = 1,
                idCompteRecepteur = 3,
                montant = 200,
                dateCreation = new DateTime(),
                designation = "transfert 1"

            });

            Assert.IsTrue(resualtat);// le update a resussie
            Assert.IsTrue(comptes.ElementAt(0).solde == 900); // expediteur
            Assert.IsTrue(comptes.ElementAt(1).solde == 1900);// l'ancien recepteur
            Assert.IsTrue(comptes.ElementAt(2).solde == 3200);// le nouveau recepteur
        }

        [TestMethod]
        public void update_montant_et_expediteur_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(3); // changment dans le montant et l'expediteur
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transferts.FirstOrDefault(t => t.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Add(It.IsAny<Transfert>())).Returns((Transfert t) => { transferts.Add(t); return t; });

            transfertRepository = new TransfertRepository(moqDbContext.Object, moqDbSetTranfert.Object, moqDbSetCompte.Object);

            bool resualtat = transfertRepository.Update(new Transfert
            {
                id = 1,
                idCompteExpediteur = 3,
                idCompteRecepteur = 2,
                montant = 200,
                dateCreation = new DateTime(),
                designation = "transfert 1"

            });

            Assert.IsTrue(resualtat);// le update a resussie
            Assert.IsTrue(comptes.ElementAt(0).solde == 1100 ); // l'ancien expediteur
            Assert.IsTrue(comptes.ElementAt(1).solde == 2100 );// le recepteur
            Assert.IsTrue(comptes.ElementAt(2).solde == 2800); // le nouveau expediteur
            
        }
        [TestMethod]
        public void update_montant_et_expediteur_et_recepteur_test()
        {
            moqDbContext = new Mock<DbContextEntities>();
            moqDbSetTranfert = new Mock<DbSet<Transfert>>();
            moqDbSetCompte = new Mock<DbSet<Compte>>();

            moqDbContext.Setup(c => c.SaveChanges()).Returns(5); // changment dans le montant et l'expediteur et le recepteur
            moqDbSetCompte.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => comptes.FirstOrDefault(compte => compte.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Find(It.IsAny<Object[]>())).Returns((Object[] ids) => transferts.FirstOrDefault(t => t.id == (int)ids[0]));
            moqDbSetTranfert.Setup(m => m.Add(It.IsAny<Transfert>())).Returns((Transfert t) => { transferts.Add(t); return t; });

            transfertRepository = new TransfertRepository(moqDbContext.Object, moqDbSetTranfert.Object, moqDbSetCompte.Object);

            bool resualtat = transfertRepository.Update(new Transfert
            {
                id = 1,
                idCompteExpediteur = 3,
                idCompteRecepteur = 4,
                montant = 200,
                dateCreation = new DateTime(),
                designation = "transfert 1"

            });

            Assert.IsTrue(resualtat);// le update a resussie
            Assert.IsTrue(comptes.ElementAt(0).solde == 1100); // l'ancien expediteur
            Assert.IsTrue(comptes.ElementAt(1).solde == 1900); // l'ancien recepteur
            Assert.IsTrue(comptes.ElementAt(2).solde ==2800 ); // le nouveau expediteur
            Assert.IsTrue(comptes.ElementAt(3).solde == 4200); // le nouveau recepteur
            

        }
        
        
    }
}
