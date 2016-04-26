using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UnityTest_v1.Models;

namespace UnityTest_v1
{
    public class TransactionRepository : IGenericRepository<Transactions>
    {

        private DbContextEntities dbContext;
        private DbSet<Transactions> dbSetTransactions;
        private DbSet<Compte> dbSetCompte;

        private const string DEPENSE = "DEPENSE";
        private const string ENTREE = "ENTREE";




        public TransactionRepository()
        {
            this.dbContext = new DbContextEntities();
            dbSetCompte = dbContext.Compte;
            dbSetTransactions = dbContext.Transactions;

        }

        public TransactionRepository(DbContextEntities dbContext, DbSet<Transactions> dbSetTransactions,
                                    DbSet<Compte> dbSetCompte)
        {
            this.dbContext = dbContext;
            this.dbSetTransactions = dbSetTransactions;
            this.dbSetCompte = dbSetCompte;

        }

        public TransactionRepository(DbContextEntities dbContext)
        {
            dbSetCompte = dbContext.Compte;
            dbSetTransactions = dbContext.Transactions;
        }

        public IEnumerable<Transactions> GetAll()
        {
            return dbSetTransactions.ToList();
        }

        public Transactions GetByID(Object id)
        {
            int identifiant = (int)id;
            return dbSetTransactions.FirstOrDefault(t => t.id == identifiant);
        }

        public bool Insert(Transactions entity)
        {
            
            Compte compteCourant = dbSetCompte.Find(entity.idCompte);
            double deltaSolde = entity.typeTransact.ToUpper().Equals(DEPENSE) ? -entity.montant : entity.montant;
            compteCourant.solde += deltaSolde; //maj du compte
            dbSetTransactions.Add(entity); // ajout de la transaction

            if (dbContext.SaveChanges() == 2)
            {
                return true;
            }

            return false;
        }

        public bool Delete(object id)
        {
            int identifiant = (int)id;

            Transactions transactionCourante = dbSetTransactions.Find(identifiant);
            Delete(transactionCourante);
            if (dbContext.SaveChanges() == 2)
            {
                return true;
            }

            return false;
        }

        public void Delete(Transactions entityToDelete)
        {
            Compte compteCourant = dbSetCompte.Find(entityToDelete.idCompte);

            double deltaSolde = entityToDelete.typeTransact.ToUpper().Equals(DEPENSE) ? entityToDelete.montant : -entityToDelete.montant;
            compteCourant.solde += deltaSolde; //maj du compte
            dbSetTransactions.Remove(entityToDelete); //suppression de la transaction 
            

        }

        public bool Update(Transactions entityToUpdate)
        {
            Transactions ancienneTransaction = dbSetTransactions.Find(entityToUpdate.id);
            Compte ancienCompte = dbSetCompte.Find(ancienneTransaction.idCompte);
            Compte nouveauCompte = dbSetCompte.Find(entityToUpdate.idCompte);
            int majAttendues = 1 ; 
 
            // annuler l'effet de l'ancienne transaction
            double deltaSolde = ancienneTransaction.typeTransact.ToUpper().Equals(DEPENSE) ? ancienneTransaction.montant :
                                                                                 - ancienneTransaction.montant;
            ancienCompte.solde += deltaSolde;
            
            // ajouter l'effet de la nouvelle transaction
            deltaSolde = entityToUpdate.typeTransact.ToUpper().Equals(DEPENSE) ? - entityToUpdate.montant :
                                                                         entityToUpdate.montant;
            nouveauCompte.solde += deltaSolde;

            if (nouveauCompte.id != ancienCompte.id)
                majAttendues += 2;
            else
                majAttendues++;
                

            //update de la nouvelle transaction

            ancienneTransaction.idCategorie = entityToUpdate.idCategorie;
            ancienneTransaction.dateCreation = entityToUpdate.dateCreation;
            ancienneTransaction.designation = entityToUpdate.designation;
            ancienneTransaction.montant = entityToUpdate.montant;
            ancienneTransaction.typeTransact = entityToUpdate.typeTransact;
            ancienneTransaction.idCompte = entityToUpdate.idCompte;

            if (dbContext.SaveChanges() == majAttendues)
                return true;
            else
                return false;

            
        }

        public IEnumerable<Transactions> FindBy(System.Linq.Expressions.Expression<Func<Transactions, bool>> predicate)
        {
            return dbSetTransactions.Where(predicate);
        }
    }
}