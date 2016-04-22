var app = angular.module('MizaniaProjectModule', ['ngRoute']);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/Home", {
            templateUrl: "/Home.html",
            controller: "HomeController"
        })
        .when("/Users", {
            templateUrl: "ListeUsers.html",
            controller: "UserBDController"
        })
        .when("/ModifyUser/:id", {
            templateUrl: "UserUpdate.html",
            controller: "UserBDController"
        })
        .when("/NewAccount", {
            templateUrl: "AjouterCompte.html",
            controller: "AccountBDController"
        })
         .when("/UserAccounts/:id", {
             templateUrl: "ListeComptesUser.html",
             controller: "UserBDController"
         })
        .when("/Accounts", {
            templateUrl: "ListeComptes.html",
            controller: "AccountBDController"
        })
        .when("/CategoriesList", {
            templateUrl: "ListeCategories.html",
            controller: "CategorieBDController"
        })


        
    .otherwise({ redirectTo: "/Home" })
});
