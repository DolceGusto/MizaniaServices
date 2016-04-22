using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnityTest_v1.Models; 
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace UnityTest_v1.Controllers
{
    public class CompteController : ApiController
    {
         private ICompteService _compteService;
        

        public CompteController()
        {
           _compteService = IoC_Config.Container.Resolve<ICompteService>();
        }

        [HttpGet] /* Retourne tous les utilisateurs */
        public List<Compte> getAllAccounts()
        {
            return _compteService.GetAllAccounts().ToList();
        }

        [HttpGet] 
        public Compte getOneAccount(int id)
        {
            return _compteService.GetAccountById(id); 
        }

       
        [HttpPost]  /*Permet d'ajouter un utilisateur */
        public HttpResponseMessage addAccount([FromBody]Compte account)
        {

            try
            {
                if (!_compteService.AddAccount(account))
                {
                    throw new Exception("ajout de l'instance du compte non effecuté ");
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.StatusCode = HttpStatusCode.Created;
                String uri = Url.Link("GetAccount", new { id = account.id });
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
        public HttpResponseMessage deleteAccount(int id)
        {
            try
            {
                if (!_compteService.DeleteAccount(id))
                {
                    throw new Exception(" Impossible de supprimer le compte dont ID = " + id);
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

        [HttpPost]  /*Permet d'ajouter un utilisateur */
        public HttpResponseMessage addUserV(Newtonsoft.Json.Linq.JArray data)

        {
            Utilisateur user = JsonConvert.DeserializeObject<Utilisateur>(data[0].ToString(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            Compte compte = JsonConvert.DeserializeObject<Compte>(data[1].ToString(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
          
           /* Utilisateur user = data["user"].ToObject<Utilisateur>();
            Compte compte = data["compte"].ToObject<Compte>();*/

            try
            {
                if (!_compteService.CreerUtilisateur(user, compte))
                {
                    throw new Exception("ajout de l'instance de l'utilisateur non effecuté " + compte.designation);
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                
                return response;

            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);

                return response;
            }
        }

        [HttpPut]
        public HttpResponseMessage updateAccount(Compte account)
        {
            try
            {

                if (!_compteService.UpdateAccount(account))
                {

                    throw new Exception("Mise à jour du compte échouée");
                }
                Compte compte = _compteService.GetAccountById(account.id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, compte);
                response.StatusCode = HttpStatusCode.OK;
                String uri = Url.Link("GetUser", new { id = compte.id });
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