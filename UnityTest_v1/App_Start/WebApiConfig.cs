using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace UnityTest_v1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            config.Routes.MapHttpRoute(
              name: "GetAllUsers",
              routeTemplate: "api/User/getAll/",
              defaults: new
              {
                  controller = "User",
                  action = "getAllUsers"
              }
           );

            config.Routes.MapHttpRoute(
               name: "GetUser",
               routeTemplate: "api/User/getOneUser/{id}",
               defaults: new
               {
                   controller = "User",
                   action = "getOneUser"

               },
               constraints: new { id = @"\d+" }

            );

            config.Routes.MapHttpRoute(
                 name: "PostUser",
                 routeTemplate: "api/User/addUser",
                 defaults: new
                 {
                     controller = "User",
                     action = "addUser"
                 }
              );

            config.Routes.MapHttpRoute(
                  name: "DeleteUser",
                  routeTemplate: "api/User/deleteUser/{id}",
                  defaults: new
                  {
                      controller = "User",
                      action = "deleteUser",

                  },
                   constraints: new
                   {
                       id = @"\d+",
                   }
               );

            config.Routes.MapHttpRoute(
                 name: "PutUser",
                 routeTemplate: "api/User/updateUser/{id}",
                 defaults: new
                 {
                     controller = "User",
                     action = "updateUser"
                 },
              constraints: new { id = @"\d+" }
              );

            config.Routes.MapHttpRoute(
              name: "GetUserByNom",
              routeTemplate: "api/User/getUserByNom/{name}",
              defaults: new
              {
                  controller = "User",
                  action = "getUserByNom"
              }
           );

            config.Routes.MapHttpRoute(
             name: "GetUserByPrenom",
             routeTemplate: "api/User/getUserByPrenom/{prenom}",
             defaults: new
             {
                 controller = "User",
                 action = "getUserByPrenom"
             }
          );


            config.Routes.MapHttpRoute(
             name: "GetUserPortefeuille",
             routeTemplate: "api/User/getUserPortefeuille/{id}",
             defaults: new
             {
                 controller = "User",
                 action = "getUserPortefeuille",

             },
               constraints: new { id = @"\d+" }
          );

            config.Routes.MapHttpRoute(
             name: "GetUserAccounts",
             routeTemplate: "api/User/getUserAccounts/{id}",
             defaults: new
             {
                 controller = "User",
                 action = "getUserAccounts"

             },
               constraints: new { id = @"\d+" }
          );















            config.Routes.MapHttpRoute(
              name: "GetAllAccounts",
              routeTemplate: "api/Compte/getAll/",
              defaults: new
              {
                  controller = "Compte",
                  action = "getAllAccounts"
              }
           );

            config.Routes.MapHttpRoute(
               name: "GetAccount",
               routeTemplate: "api/User/getOneAccount/{id}",
               defaults: new
               {
                   controller = "Compte",
                   action = "getOneAccount"

               },
               constraints: new { id = @"\d+" }

            );

            config.Routes.MapHttpRoute(
                 name: "PostAccount",
                 routeTemplate: "api/Compte/addAccount",
                 defaults: new
                 {
                     controller = "Compte",
                     action = "addAccount"
                 }
              );

            config.Routes.MapHttpRoute(
                  name: "DeleteAccount",
                  routeTemplate: "api/Compte/deleteAccount/{id}",
                  defaults: new
                  {
                      controller = "Compte",
                      action = "deleteAccount",

                  },
                   constraints: new
                   {
                       id = @"\d+",
                   }
               );

            config.Routes.MapHttpRoute(
                 name: "PutAccount",
                 routeTemplate: "api/Compte/updateAccount/{id}",
                 defaults: new
                 {
                     controller = "Compte",
                     action = "updateAccount"
                 },
              constraints: new { id = @"\d+" }
              );

           
            config.Routes.MapHttpRoute(
                name: "PostUser2",
                routeTemplate: "api/Compte/addUserV",
                defaults: new
                {
                    controller = "Compte",
                    action = "addUserV"
                }
             );




            config.Routes.MapHttpRoute(
              name: "GetAllCategories",
              routeTemplate: "api/Categorie/getAll/",
              defaults: new
              {
                  controller = "Categorie",
                  action = "getAllCategories"
              }
           );

            config.Routes.MapHttpRoute(
               name: "GetCategorie",
               routeTemplate: "api/Categorie/getOneCategorie/{id}",
               defaults: new
               {
                   controller = "Categorie",
                   action = "getOneCategorie"

               },
               constraints: new { id = @"\d+" }

            );

            config.Routes.MapHttpRoute(
                 name: "PostCategorie",
                 routeTemplate: "api/Categorie/addCategorie",
                 defaults: new
                 {
                     controller = "Categorie",
                     action = "addCategorie"
                 }
              );

            config.Routes.MapHttpRoute(
                  name: "DeleteCategorie",
                  routeTemplate: "api/Categorie/deleteCategorie/{id}",
                  defaults: new
                  {
                      controller = "Categorie",
                      action = "deleteCategorie",

                  },
                   constraints: new
                   {
                       id = @"\d+",
                   }
               );

            config.Routes.MapHttpRoute(
                 name: "PutCategorie",
                 routeTemplate: "api/Categorie/updateCategorie/{id}",
                 defaults: new
                 {
                     controller = "Categorie",
                     action = "updateAccount"
                 },
              constraints: new { id = @"\d+" }
              );

        }
    }
}
