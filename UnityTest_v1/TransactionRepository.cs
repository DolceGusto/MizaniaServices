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

        private const string DEPENSE = "depense";
        private const string ENTREE = "entree";




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
            
            Compte compteCourant = dbSetCompte.FirstOrDefault(compte => compte.id == entity.idCompte);
            double deltaSolde = entity.typeTransact.Equals(DEPENSE) ? -entity.montant : entity.montant;
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
            

            Transactions transactionCourante = dbSetTransactions.FirstOrDefault(t => t.id == identifiant);
            Delete(transactionCourante);
            if (dbContext.SaveChanges() == 2)
            {
                return true;
            }

            return false;
        }

        public void Delete(Transactions entityToDelete)
        {
            Compte compteCourant = dbSetCompte.FirstOrDefault(c => c.id == entityToDelete.idCompte);

            double deltaSolde = entityToDelete.typeTransact.Equals(DEPENSE) ? entityToDelete.montant : -entityToDelete.montant;
            compteCourant.solde += deltaSolde; //maj du compte
            dbSetTransactions.Remove(entityToDelete); //suppression de la transaction 
            

        }

        public bool Update(Transactions entityToUpdate)
        {
            Transactions ancienneTransaction = dbSetTransactions.FirstOrDefault(t => t.id == entityToUpdate.id);
            Compte nouveauCompte = dbSetCompte.FirstOrDefault(c => c.id == entityToUpdate.idCompte);
            Compte ancienCompte = dbSetCompte.FirstOrDefault(c => c.id == ancienneTransaction.idCompte);
            double deltaSolde;
            //changment dans le compte 
            if (ancienCompte.id != nouveauCompte.id)
            {
                deltaSolde = ancienneTransaction.typeTransact.Equals(DEPENSE) ?  ancienneTransaction.montant : - ancienneTransaction.montant ;
                ancienCompte.solde += deltaSolde; //maj de l'ancien compte

                deltaSolde = entityToUpdate.typeTransact.Equals(DEPENSE) ? -entityToUpdate.montant : entityToUpdate.montant;
                nouveauCompte.solde += deltaSolde ; //maj du nouveau compte

                if (dbContext.SaveChanges() == 2)
                {
                    return true;
                }
                return false;

            }else if(! ancienneTransaction.typeTransact
                       .Equals(entityToUpdate.typeTransact) ){ // changment dans le type de la transaction 

                deltaSolde = ancienneTransaction.typeTransact.Equals(DEPENSE) ? ancienneTransaction.montant : - ancienneTransaction.montant ;
                nouveauCompte.solde += deltaSolde; //annulation de l'ancienne valeur de la transaction 

                deltaSolde = entityToUpdate.typeTransact.Equals(DEPENSE) ? - entityToUpdate.montant : entityToUpdate.montant ;
                nouveauCompte.solde += deltaSolde; // maj de la nouvelle valeur de la transaction

                if (dbContext.SaveChanges() == 1)
                {
                    return true;
                }
                return false;

            }else if(ancienneTransaction.montant - entityToUpdate.montant
                      > double.Epsilon ){ // changment dans le montant de la transaction 

                deltaSolde = entityToUpdate.typeTransact.Equals(DEPENSE) ? -(entityToUpdate.montant - ancienneTransaction.montant) : 
                                                                          entityToUpdate.montant - ancienneTransaction.montant ;

                nouveauCompte.solde += deltaSolde; //maj du compte

                if (dbContext.SaveChanges() == 1)
                {
                    return true;
                }
                return false;

            }

            return true; //  pas de update 
        }

        public IEnumerable<Transactions> FindBy(System.Linq.Expressions.Expression<Func<Transactions, bool>> predicate)
        {
            return dbSetTransactions.Where(predicate);
        }
    }
}