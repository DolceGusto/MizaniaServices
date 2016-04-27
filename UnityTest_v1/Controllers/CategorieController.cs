using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnityTest_v1.Models;
using Microsoft.Practices.Unity;
using UnityTest_v1.IServices;
using System.Diagnostics;
namespace UnityTest_v1.Controllers
{
    public class CategorieController : ApiController
    {
        private ICategorieService _categorieService;
        

        public CategorieController()
        {
           _categorieService = IoC_Config.Container.Resolve<ICategorieService>();
        }

        [HttpGet] /* Retourne tous les catégories de transactions */
        public List<Categorie> getAllCategories()
        {
            //add try catch here to specify the statut of the return and log it
            return _categorieService.GetAllCategories().ToList();
        }

        [HttpGet] 
        public Categorie getOneCategorie(int id)
        {
            //add try catch here to specify the statut of the return and log it
            return _categorieService.GetCategorieById(id); 
        }

       
        [HttpPost]  /*Permet d'ajouter une catégorie */
        public HttpResponseMessage addCategorie([FromBody]Categorie categorie)
        {

            try
            {
                if (!_categorieService.AddCategorie(categorie))
                {
                    Trace.Fail("ajout de l'instance de catégorie non effectué ");
                    throw new Exception("ajout de l'instance de catégorie non effectué ");
                }
                Trace.TraceInformation("ajout de l'instance de catégorie effectué ");
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.StatusCode = HttpStatusCode.Created;
                String uri = Url.Link("GetCategorie", new { id = categorie.id });
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
        public HttpResponseMessage deleteCategorie(int id)
        {
            try
            {
                if (!_categorieService.DeleteCategorie(id))
                {
                    Trace.Fail(" Impossible de supprimer la catégorie dont ID = " + id);
                    throw new Exception(" Impossible de supprimer la catégorie dont ID = " + id);
                }
                Trace.TraceInformation(" Suppression de la catégorie dont ID = " +id+" ");
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
        public HttpResponseMessage updateCategorie(Categorie categorie)
        {
            try
            {

                if (!_categorieService.UpdateCategorie(categorie))
                {

                    throw new Exception("Mise à jour du compte échouée");
                }
                Categorie category = _categorieService.GetCategorieById(categorie.id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, category);
                response.StatusCode = HttpStatusCode.OK;
                String uri = Url.Link("GetCategorie", new { id = category.id });
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