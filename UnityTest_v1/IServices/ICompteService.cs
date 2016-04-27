using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityTest_v1.Models; 

namespace UnityTest_v1
{
    interface ICompteService
    {
        Compte GetAccountById(int AccountId);
        IEnumerable<Compte> GetAllAccounts();
        bool AddAccount(Compte compte);
        bool DeleteAccount(int id);
        bool UpdateAccount(Compte compte);
        IEnumerable<Compte> FindAccountsByUserID(int id);
        bool CreerUtilisateur(Utilisateur user, Compte compte);
       
    }
}
