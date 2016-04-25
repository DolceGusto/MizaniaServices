using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityTest_v1.Models;

namespace UnityTest_v1.IServices
{
    public interface ITransfertService
    {
        Transfert GetTransfertById(int transfertId);
        IEnumerable<Transfert> GetAllTransferts();
        bool AddTransfert(Transfert transfert);
        bool DeleteTransfert(int id);
        bool UpdateTransfert(Transfert transfert);
    }
}