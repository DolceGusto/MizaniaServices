using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UnityTest_v1.IServices;
using UnityTest_v1.Models;

namespace UnityTest_v1.Services
{
    public class TransactionsService : ITransactionService 
    {

        private TransactionRepository transactionRepository;



        public TransactionsService()
        {
            transactionRepository = new TransactionRepository();
        }

        public TransactionsService(DbContextEntities dbContext, 
                                   DbSet<Transactions> dbSetTransactions,
                                   DbSet<Compte> dbSetCompte )
        {
            transactionRepository = new TransactionRepository(dbContext,dbSetTransactions,dbSetCompte);
        }

        public Models.Transactions GetTransactionById(int transactionId)
        {
           return transactionRepository.GetByID(transactionId);
        }

        public IEnumerable<Models.Transactions> GetAllTransactions()
        {
            return transactionRepository.GetAll();
        }

        public bool AddTransaction(Models.Transactions transaction)
        {
           return transactionRepository.Insert(transaction);
        }

        public bool DeleteTransaction(int id)
        {
            return transactionRepository.Delete(id);
        }

        public bool UpdateTransaction(Models.Transactions transaction)
        {
            return transactionRepository.Update(transaction);
        }
    }
}