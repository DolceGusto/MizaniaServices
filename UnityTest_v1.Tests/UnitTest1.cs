using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using UnityTest_v1.Models;
using Moq;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;

namespace UnityTest_v1.Tests
{
    [TestClass]
    public class PorteFeuilleRepositoryTest
    {

        private GenericRepository<PorteFeuille> porteFeuilleRepo;
        private Mock<DbContextEntities> moqContext;
        private Mock<DbSet<PorteFeuille>> moqSetPorteFeuille;
        private List<PorteFeuille> data;


        [ClassInitialize]
        public static void InitializeTest(TestContext testContext)
        {

        }

        [TestInitialize]
        public void InitializeTest()
        {


            data = new List<PorteFeuille>
            {
                new PorteFeuille { designation="famille miller",id=1,dateCreation= new DateTime() },
                new PorteFeuille { designation="famille smith",id=2 ,dateCreation= new DateTime()  }

            };

            moqSetPorteFeuille = new Mock<DbSet<PorteFeuille>>();
            moqContext = new Mock<DbContextEntities>();
        }

        [TestMethod]
        public void getById_test()
        {
            
            // moqer le find du dbSet afin de pouvoir l'utiliser dans la methode getById de GenericRepository
            moqSetPorteFeuille.Setup(m => m.Find(1)).Returns( data.AsQueryable().FirstOrDefault(p => p.id == 1));
            porteFeuilleRepo = new GenericRepository<PorteFeuille>(moqContext.Object,moqSetPorteFeuille.Object);

            PorteFeuille porteFeuille = porteFeuilleRepo.GetByID(1);

            Assert.IsTrue(porteFeuille.id == 1);
            Assert.AreEqual(porteFeuille.designation, "famille miller");
            

        }


        [TestMethod]
        public void add_test()
        {

            moqSetPorteFeuille.Setup(m => m.Add(It.IsAny<PorteFeuille>())).Returns((PorteFeuille p) => { data.Add(p); return p; });
            moqContext.Setup(c => c.SaveChanges()).Returns(1); // la methode saveChanges dans la methode genericRepo.Insert()
                                                               // retourne 1 par default 

            porteFeuilleRepo = new GenericRepository<PorteFeuille>(moqContext.Object, moqSetPorteFeuille.Object);

            bool resulataInsert = porteFeuilleRepo.Insert(new PorteFeuille
            { id = 3, designation = "famille dupont", dateCreation = new DateTime() });

            Assert.IsTrue(resulataInsert);

            /* si on veut tester que l'entite est bien ajouté il va faloir modifier le code de ce test ci : en ajoutant
             * le code du test de GetById mais dés lors que c'est un test unitaire (une seul méthode) donc cela n'est pas 
             * vraiment necessaire 
             */
        }

        [TestMethod]
        public void getAll_test()
        {
            //moqer le dbset en tant qu'objet queryable 
            moqSetPorteFeuille.As<IQueryable<PorteFeuille>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            moqSetPorteFeuille.As<IQueryable<PorteFeuille>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            moqSetPorteFeuille.As<IQueryable<PorteFeuille>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            moqSetPorteFeuille.As<IQueryable<PorteFeuille>>().Setup(m => m.GetEnumerator()).Returns(data.AsQueryable().GetEnumerator());

            porteFeuilleRepo = new GenericRepository<PorteFeuille>(moqContext.Object, moqSetPorteFeuille.Object);

            IEnumerable<PorteFeuille> porteFeuilles = porteFeuilleRepo.GetAll();

            Assert.IsTrue(porteFeuilles.Count() == 2);
            Assert.IsTrue(porteFeuilles.ToList().ElementAt(0).id == data.ElementAt(0).id);
            Assert.IsTrue(porteFeuilles.ToList().ElementAt(1).id == data.ElementAt(1).id);
            Assert.AreEqual(porteFeuilles.ToList().ElementAt(0).designation , data.ElementAt(0).designation);
            Assert.AreEqual(porteFeuilles.ToList().ElementAt(1).designation, data.ElementAt(1).designation);
            
        }
    }


}
