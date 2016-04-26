using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UnityTest_v1.Models;

namespace UnityTest_v1
{
    public class TransfertRepository : IGenericRepository<Transfert>
    {
        private DbContextEntities dbContext;
        private DbSet<Transfert> dbSetTransfert;
        private DbSet<Compte> dbSetCompte;
        


        public TransfertRepository(){

            dbContext = new DbContextEntities();
            dbSetCompte = dbContext.Compte;
            dbSetTransfert = dbContext.Transfert;
        }

        public TransfertRepository(DbContextEntities dbContext,
                                    DbSet<Transfert> dbSetTransfert,
                                    DbSet<Compte> dbSetCompte)
        {
            this.dbContext = dbContext;
            this.dbSetCompte = dbSetCompte;
            this.dbSetTransfert = dbSetTransfert;
        }

        public IEnumerable<Transfert> GetAll()
        {
            return dbSetTransfert.ToList();
        }

        public Transfert GetByID(object id)
        {
            int identifiant = (int) id ;
                
            return dbSetTransfert.FirstOrDefault(t => t.id == identifiant);
        }

        public bool Insert(Transfert entity)
        {
            Compte compteRecepteur  = dbSetCompte.Find(entity.idCompteRecepteur) ;
            Compte compteExpediteur = dbSetCompte.Find(entity.idCompteExpediteur) ;

            compteExpediteur.solde -= entity.montant; //maj du compte expediteur
            compteRecepteur.solde += entity.montant; //maj du compte recepteur 

            dbSetTransfert.Add(entity);
            if (dbContext.SaveChanges() == 3)
            {
                return true;
            }
            return false;
        }

        public bool Delete(object id)
        {
            int identifiant = (int)id;

            Transfert transfertToDelete = dbSetTransfert.Find(identifiant);
            Delete(transfertToDelete);
            if (dbContext.SaveChanges() == 3)
            {
                return true;
            }
            return false;

        }

        public void Delete(Transfert entityToDelete)
        {
            Compte compteExpediteur = dbSetCompte.Find(entityToDelete.idCompteExpediteur);
            Compte compteRecepteur = dbSetCompte.Find(entityToDelete.idCompteRecepteur);

            compteExpediteur.solde += entityToDelete.montant;  //maj du compte expediteur 
            compteRecepteur.solde -= entityToDelete.montant; // maj du compte recepteur ;
            dbSetTransfert.Remove(entityToDelete);


        }

        public bool Update(Transfert entityToUpdate)
        {
            Transfert ancienTransfert = dbSetTransfert.Find(entityToUpdate.id);
            Compte ancienCompteExpediteur = dbSetCompte.Find(ancienTransfert.idCompteExpediteur);
            Compte ancienCompteRecepteur = dbSetCompte.Find(ancienTransfert.idCompteRecepteur);
            Compte nouveauCompteExpediteur = dbSetCompte.Find(entityToUpdate.idCompteExpediteur);
            Compte nouveauCompteRecepteur = dbSetCompte.Find(entityToUpdate.idCompteRecepteur);

            int majAttendu = 1; //compte le nombre de mis à jour attendues(celle du transfert par defaut)
                                // celle des deux compte en cas d'un changment dans le montant

            ancienCompteExpediteur.solde += ancienTransfert.montant;
            nouveauCompteExpediteur.solde -= entityToUpdate.montant;
            ancienCompteRecepteur.solde -= ancienTransfert.montant;
            nouveauCompteRecepteur.solde += entityToUpdate.montant;
            
            ancienTransfert.idCompteExpediteur = entityToUpdate.idCompteExpediteur;
            ancienTransfert.idCompteRecepteur = entityToUpdate.idCompteRecepteur;
            ancienTransfert.montant = entityToUpdate.montant;
            ancienTransfert.designation = entityToUpdate.designation;
            ancienTransfert.dateCreation = ancienTransfert.dateCreation;

            if (ancienCompteExpediteur.id != nouveauCompteExpediteur.id)
                majAttendu += 2;
            if (ancienCompteRecepteur.id != nouveauCompteRecepteur.id)
                majAttendu += 2;
            if (ancienCompteRecepteur.id == nouveauCompteRecepteur.id &&
               ancienCompteExpediteur.id == nouveauCompteExpediteur.id)
                majAttendu += 2;

            if (dbContext.SaveChanges() == majAttendu)
            {
                return true;
            }
            return false;

        }

        public IEnumerable<Transfert> FindBy(System.Linq.Expressions.Expression<Func<Transfert, bool>> predicate)
        {
            return dbSetTransfert.Where(predicate);
        }
    }
}