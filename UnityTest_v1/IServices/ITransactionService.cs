using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityTest_v1.Models;

namespace UnityTest_v1.IServices
{
    public interface ITransactionService
    {
        Transactions GetTransactionById(int transactionId);
        IEnumerable<Transactions> GetAllTransactions();
        bool AddTransaction(Transactions transaction);
        bool DeleteTransaction(int id);
        bool UpdateTransaction(Transactions transaction);
    }
}