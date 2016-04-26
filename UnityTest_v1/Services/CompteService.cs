<<<<<<< HEAD:UnityTest_v1/Services/CompteService.cs
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityTest_v1.Models;
using UnityTest_v1.IServices;

namespace UnityTest_v1
{
    public class CompteService: ICompteService
    {
        private IGenericRepository<Utilisateur> _userRepository;
        private IGenericRepository<Compte> _compteRepository;

       public CompteService(IGenericRepository<Utilisateur> userRepository, IGenericRepository<Compte> compteRepository)
       {
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
        
       public bool AddAccount(Compte compte)
       {
           if (_compteRepository.Insert(compte))
               return true;
           else return false;
       }
         

       public bool DeleteAccount(int id)
       {
           if (_compteRepository.Delete(id))
               return true; 
           else return false; 
          
       }
        

       public bool UpdateAccount(Compte compte)
       {
           if (_compteRepository.Update(compte))
               return true;
           else return false; 
       }

       public IEnumerable<Compte> FindAccountsByUserID(int id)
       {
           return _compteRepository.FindBy(d => d.idUtilisateur ==id);
       }

       
       
    }
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityTest_v1.Models;
using UnityTest_v1.IServices;

namespace UnityTest_v1
{
    public class CompteService: ICompteService
    {
        private IGenericRepository<Utilisateur> _userRepository;
        private IGenericRepository<Compte> _compteRepository;

       public CompteService(IGenericRepository<Utilisateur> userRepository, IGenericRepository<Compte> compteRepository)
       {
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
        
       public bool AddAccount(Compte compte)
       {
           if (_compteRepository.Insert(compte))
               return true;
           else return false;
       }
         

       public bool DeleteAccount(int id)
       {
           if (_compteRepository.Delete(id))
               return true; 
           else return false; 
          
       }
        

       public bool UpdateAccount(Compte compte)
       {
           if (_compteRepository.Update(compte))
               return true;
           else return false; 
       }

       public IEnumerable<Compte> FindAccountsByUserID(int id)
       {
           return _compteRepository.FindBy(d => d.idUtilisateur ==id);
       }

       
       
    }
>>>>>>> 5597514c2bf9093f555916b1ed1b3425ad0b830d:UnityTest_v1/Services/CompteService.cs
}