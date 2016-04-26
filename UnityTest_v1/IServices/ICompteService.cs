<<<<<<< HEAD:UnityTest_v1/IServices/ICompteService.cs
﻿using System;
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
=======
﻿using System;
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
>>>>>>> 5597514c2bf9093f555916b1ed1b3425ad0b830d:UnityTest_v1/IServices/ICompteService.cs
