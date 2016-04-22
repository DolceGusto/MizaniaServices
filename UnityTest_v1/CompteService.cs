using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityTest_v1.Models; 

namespace UnityTest_v1
{
    public class CompteService: ICompteService
    {
      //  private IUnitOfWork _unitOfWork;
        private IGenericRepository<Utilisateur> _userRepository;
        private IGenericRepository<Compte> _compteRepository;

       public CompteService(/*IUnitOfWork unitOfWork, */IGenericRepository<Utilisateur> userRepository, IGenericRepository<Compte> compteRepository)
       {
          // this._unitOfWork = unitOfWork;
           this._userRepository = userRepository;
           this._compteRepository = compteRepository; 
       }


       public bool CreerUtilisateur(Utilisateur user, Compte compte)
       {
           if (_userRepository.Insert(user) && _compteRepository.Insert(compte))
               return true;
           else return false;
       }

       public IEnumerable<Compte> GetAllAccounts()
       {
           return _compteRepository.GetAll();
       }

       public Compte GetAccountById(int AccountId)
       {
           return _compteRepository.GetByID(AccountId);
       }

      /* public bool AddAccount(Compte compte)
       {
           _compteRepository.Insert(compte);
           if (_unitOfWork.Save() == true) return true;
           else return false;
       }

       public bool DeleteAccount(int id)
       {
           _compteRepository.Delete(id);
           if (_unitOfWork.Save() == true) return true;
           else return false;
       }

       public bool UpdateAccount(Compte compte)
       {
           _compteRepository.Update(compte);
           if (_unitOfWork.Save() == true) return true;
           else return false;
       }

       public IEnumerable<Compte> FindAccountsByUserID(int id)
       {
           return _compteRepository.FindBy(d => d.idUtilisateur ==id);
       }

       
       public void Dispose()
       {
           this._unitOfWork.Dispose();
       }*/
    }
}