using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace UnityTest_v1.App_Start.RouteAnnuaire
{
    public class RouteTransactions
    {
        public static void addRoutesTransactions(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
              name: "GetAllTransaction",
              routeTemplate: "api/Transaction/getAll/",
              defaults: new
              {
                  controller = "Transaction",
                  action = "getAllTransactions"
              }
           );

            config.Routes.MapHttpRoute(
                name :"GetTransaction",
                routeTemplate: "api/Transaction/getOneTransaction/{id}",
                defaults: new
                {
                  controller = "Transaction",
                  action = "getOneTransaction"
                },
                constraints: new { id = @"\d+" }

            );

            config.Routes.MapHttpRoute(
                name:"PostTransaction",
                routeTemplate: "api/Transaction/addTransaction",
                defaults: new
                {
                    controller = "Transaction",
                    action = "addTransaction"
                }  
             );

            config.Routes.MapHttpRoute(
                name: "DeleteTransaction",
                routeTemplate: "api/Transaction/deleteTransaction",
                defaults: new
                {
                    controller = "Transaction",
                    action = "deleteTransaction"
                }
             );

            config.Routes.MapHttpRoute(
                name: "PutTransaction",
                routeTemplate: "api/Transaction/updateTransaction/{id}",
                defaults: new
                {
                    controller = "Transaction",
                    action = "updateTransaction"
                },
                constraints : new {id = @"\d+" }
             );

        }
    }
}