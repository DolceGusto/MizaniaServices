using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityTest_v1.Models; 

namespace UnityTest_v1
{
    public class UserService : IUserService
    {
       // private IUnitOfWork _unitOfWork;
        private IGenericRepository<Utilisateur> _userRepository;
        private IGenericRepository<Compte> _compteRepository;

       public UserService(/*IUnitOfWork unitOfWork,*/ IGenericRepository<Utilisateur> userRepository, IGenericRepository<Compte> compteRepository)
       {
          // this._unitOfWork = unitOfWork;
           this._userRepository = userRepository;
           this._compteRepository = compteRepository; 
       }

       public IEnumerable<Utilisateur> GetAllUsers()
       {
           return _userRepository.GetAll(); 
       }

       public Utilisateur GetUserById(int UserId)
       {
           return _userRepository.GetByID(UserId);
       }
        
     /*  public bool AddUser (Utilisateur user)
       {
           _userRepository.Insert(user);
           if (_unitOfWork.Save() == true) return true;
           else return false; 
        }

        public bool DeleteUser(int id)
       {
           _userRepository.Delete(id); 
           if (_unitOfWork.Save() == true) return true;
           else return false; 
       }

        public bool UpdateUser(Utilisateur user)
        {
            _userRepository.Update(user); 
            if (_unitOfWork.Save() == true) return true;
            else return false;
        }

        public IEnumerable<Utilisateur> FindByName(string nom)
        {
           return  _userRepository.FindBy(d => d.nom.Equals(nom));
        }

        public IEnumerable<Utilisateur> FindByPrenom(string prenom)
        {
            return _userRepository.FindBy(d => d.prenom.Equals(prenom));
        }

        public PorteFeuille FindPortefeuilleUser(int id)
        {
            return (_userRepository.FindBy(d => d.id == id).SingleOrDefault()).PorteFeuille;
        }

        public IEnumerable<Compte> FindUserAccounts(int id)
        {
            return _compteRepository.FindBy(d => d.idUtilisateur == id).ToList(); 
        }

        
       public void Dispose()
       {
           this._unitOfWork.Dispose();
       }*/

    }
}