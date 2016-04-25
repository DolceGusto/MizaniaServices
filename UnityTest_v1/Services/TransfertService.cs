using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UnityTest_v1.IServices;
using UnityTest_v1.Models;

namespace UnityTest_v1.Services
{
    public class TransfertService : ITransfertService
    {

        private TransfertRepository transfertRepository;

        public TransfertService()
        {
            transfertRepository = new TransfertRepository();
        }
        
        public TransfertService(DbContextEntities dbContext,DbSet<Transfert> dbSetTransfert, DbSet<Compte> dbSetCompte  )
        {
            transfertRepository = new TransfertRepository(dbContext, dbSetTransfert, dbSetCompte);
        }
        
        public Models.Transfert GetTransfertById(int transfertId)
        {
            return transfertRepository.GetByID(transfertId);
        }

        public IEnumerable<Models.Transfert> GetAllTransferts()
        {
            return transfertRepository.GetAll();
        }

        public bool AddTransfert(Models.Transfert transfert)
        {
            return transfertRepository.Insert(transfert);
        }

        public bool DeleteTransfert(int id)
        {
            return transfertRepository.Delete(id);
        }

        public bool UpdateTransfert(Models.Transfert transfert)
        {
            return transfertRepository.Update(transfert);
        }
    }
}