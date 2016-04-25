using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace UnityTest_v1.App_Start.RouteAnnuaire
{
    public class RouteTransfert
    {

        public static void addRoutesTransferts(HttpConfiguration config)
        {

            config.Routes.MapHttpRoute(
              name: "GetAllTransfert",
              routeTemplate: "api/Transfert/getAll/",
              defaults: new
              {
                  controller = "Transfert",
                  action = "getAllTransferts"
              }
           );

            config.Routes.MapHttpRoute(
                name: "GetTransfert",
                routeTemplate: "api/Transfert/getOneTransfert/{id}",
                defaults: new
                {
                    controller = "Transfert",
                    action = "getOneTransfert"
                },
                constraints: new { id = @"\d+" }

            );

            config.Routes.MapHttpRoute(
                name: "PostTransfert",
                routeTemplate: "api/Transfert/addTransfert",
                defaults: new
                {
                    controller = "Transfert",
                    action = "addTransfert"
                }
             );

            config.Routes.MapHttpRoute(
                name: "DeleteTransfert",
                routeTemplate: "api/Transfert/deleteTransfert",
                defaults: new
                {
                    controller = "Transfert",
                    action = "deleteTransfert"
                }
             );

            config.Routes.MapHttpRoute(
                name: "PutTransfert",
                routeTemplate: "api/Transfert/updateTransfert/{id}",
                defaults: new
                {
                    controller = "Transfert",
                    action = "updateTransfert"
                },
                constraints: new { id = @"\d+" }
             );

        }

    }
}