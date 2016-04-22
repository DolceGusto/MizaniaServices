using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnityTest_v1.Models;
using Microsoft.Practices.Unity;

namespace UnityTest_v1.Controllers
{
    public class UserController : ApiController
    {
        private IUserService _userService;
        

        public UserController()
        {
           _userService = IoC_Config.Container.Resolve<IUserService>();
        }

        [HttpGet] /* Retourne tous les utilisateurs */
        public List<Utilisateur> getAllUsers()
        {
            return _userService.GetAllUsers().ToList();
        }

        [HttpGet] 
        public Utilisateur getOneUser(int id)
        {
            return _userService.GetUserById(id); 
        }

        [HttpGet] /* Retourne des urilisateurs par leurs noms */
        public List<Utilisateur> getUserByNom(string name)
        {
            return _userService.FindByName(name).ToList();
        }

        [HttpGet] /* Retourne des urilisateurs par leurs prénoms */
        public List<Utilisateur> getUserByPrenom(string prenom)
        {
            return _userService.FindByPrenom(prenom).ToList();
        }

        [HttpGet]  /* Retourne le portefeuille d'un utilisateur */
        public PorteFeuille getUserPortefeuille(int id)
        {
            return _userService.FindPortefeuilleUser(id); 
        }

        [HttpGet]  /* Retourne la liste des comptes d'un utilisateur */
        public List<Compte> getUserAccounts(int id)
        {
            return _userService.FindUserAccounts(id).ToList();
        }


        
        [HttpPost]  /*Permet d'ajouter un utilisateur */
        public HttpResponseMessage addUser([FromBody]Utilisateur user)
        {

            try
            {
                if (!_userService.AddUser(user))
                {
                    throw new Exception("ajout de l'instance de l'utilisateur non effecuté ");
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.StatusCode = HttpStatusCode.Created;
                String uri = Url.Link("GetUser", new { id = user.id });
                response.Headers.Location = new Uri(uri);

                return response;

            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);

                return response;
            }

        }

        [HttpDelete]
        public HttpResponseMessage deleteUser(int id)
        {
            try
            {
                if (!_userService.DeleteUser(id))
                {
                    throw new Exception(" Impossible de supprimer l'utilisateur dont ID = " + id);
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.NoContent);

                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response;
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);

                return response;
            }
        }

        [HttpPut]
        public HttpResponseMessage updateUser(Utilisateur user)
        {
            try
            {

                if (!_userService.UpdateUser(user))
                {

                    throw new Exception("Mise à jour de l'utilisateur échouée");
                }
                Utilisateur utilisateur = _userService.GetUserById(user.id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, utilisateur);
                response.StatusCode = HttpStatusCode.OK;
                String uri = Url.Link("GetUser", new { id = utilisateur.id });
                response.Headers.Location = new Uri(uri);

                return response;


            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);

                return response;
            }
        }


    }
}