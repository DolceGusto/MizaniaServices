using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityTest_v1.Models; 

namespace UnityTest_v1
{
    public interface IUserService /*: IDisposable*/
    {
        Utilisateur GetUserById(int UserId);
        IEnumerable<Utilisateur> GetAllUsers();
        bool AddUser (Utilisateur user);
        bool DeleteUser(int id);
        bool UpdateUser(Utilisateur user);
        IEnumerable<Utilisateur> FindByName(string nom);
        IEnumerable<Utilisateur> FindByPrenom(string prenom);
        PorteFeuille FindPortefeuilleUser(int id);
        IEnumerable<Compte> FindUserAccounts(int id);
    
    }
}
