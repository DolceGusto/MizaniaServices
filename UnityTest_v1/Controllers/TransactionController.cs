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
    public class TransactionController :ApiController
    {
            private ITransactionService _transactionService;


            public TransactionController()
            {
                _transactionService = IoC_Config.Container.Resolve<ITransactionService>();
            }


            [HttpGet]
            public List<Transactions> getAllTransactions()
            {
                return _transactionService.GetAllTransactions().ToList();
            }

            [HttpGet]
            public Transactions getOneTransaction(int id)
            {
                return _transactionService.GetTransactionById(id);
            }


            [HttpPost]
            public HttpResponseMessage addTransaction([FromBody]Transactions transaction)
            {

                try
                {
                    if (!_transactionService.AddTransaction(transaction))
                    {
                        throw new Exception("ajout de l'instance de transaction non effecuté ");
                    }
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                    response.StatusCode = HttpStatusCode.Created;
                    String uri = Url.Link("GetTransaction", new { id = transaction.id });
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
            public HttpResponseMessage deleteTransaction(int id)
            {
                try
                {
                    if (!_transactionService.DeleteTransaction(id))
                    {
                        throw new Exception(" Impossible de supprimer la transaction dont ID = " + id);
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
            public HttpResponseMessage updateTransaction(Transactions transaction)
            {
                try
                {

                    if (!_transactionService.UpdateTransaction(transaction))
                    {

                        throw new Exception("Mise à jour de la transaction échouée");
                    }
                    Transactions nouvelleTransaction = _transactionService.GetTransactionById(transaction.id);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, nouvelleTransaction);
                    response.StatusCode = HttpStatusCode.OK;
                    String uri = Url.Link("GetTransaction", new { id = nouvelleTransaction.id });
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