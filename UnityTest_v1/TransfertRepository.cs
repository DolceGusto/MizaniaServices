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
            int majAttendu = 0; //compte le nombre de mis à jour attendues

            if(ancienCompteExpediteur.id != nouveauCompteExpediteur.id){ //changement dans l'expediteur 

                ancienCompteExpediteur.solde += ancienTransfert.montant;
                nouveauCompteExpediteur.solde -= entityToUpdate.montant;
                majAttendu += 2;


            }
            if(ancienCompteRecepteur.id != nouveauCompteRecepteur.id){ //changment dans le recepteur 

                ancienCompteRecepteur.solde -= ancienTransfert.montant;
                nouveauCompteRecepteur.solde += entityToUpdate.montant;
                majAttendu += 2;

            }
            else if(entityToUpdate.montant - ancienTransfert.montant > double.Epsilon){ // changement uniquement dans le montant du transfert  

                double deltaSolde = entityToUpdate.montant - ancienTransfert.montant;
                nouveauCompteExpediteur.solde -= deltaSolde;
                nouveauCompteRecepteur.solde += deltaSolde;
                majAttendu += 2;
            }

            majAttendu += 1; // ajout de l'entite transfert 
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