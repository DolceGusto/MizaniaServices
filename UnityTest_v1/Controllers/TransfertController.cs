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
using UnityTest_v1.IServices;

namespace UnityTest_v1.Controllers
{
    public class TransfertController : ApiController 
    {
        private ITransfertService _transfertService;


        public TransfertController()
        {
            _transfertService = IoC_Config.Container.Resolve<ITransfertService>();
        }


        [HttpGet] 
        public List<Transfert> getAllTransferts()
        {
            return _transfertService.GetAllTransferts().ToList();
        }

        [HttpGet]
        public Transfert getOneTransfert(int id)
        {
            return _transfertService.GetTransfertById(id);
        }


        [HttpPost]  
        public HttpResponseMessage addTransfert([FromBody]Transfert transfert)
        {

            try
            {
                if (!_transfertService.AddTransfert(transfert))
                {
                    throw new Exception("ajout de l'instance de transfert non effecuté ");
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.StatusCode = HttpStatusCode.Created;
                String uri = Url.Link("GetTranfert", new { id = transfert.id });
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
        public HttpResponseMessage deleteTransfert(int id)
        {
            try
            {
                if (!_transfertService.DeleteTransfert(id))
                {
                    throw new Exception(" Impossible de supprimer le transfert dont ID = " + id);
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
        public HttpResponseMessage updateTransfert(Transfert transfert)
        {
            try
            {

                if (!_transfertService.UpdateTransfert(transfert))
                {

                    throw new Exception("Mise à jour du transfert échouée");
                }
                Transfert nouveauTransfert = _transfertService.GetTransfertById(transfert.id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, nouveauTransfert);
                response.StatusCode = HttpStatusCode.OK;
                String uri = Url.Link("GetTranfert", new { id = nouveauTransfert.id });
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