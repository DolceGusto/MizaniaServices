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
<<<<<<< HEAD
        .when("/ModifyAccount/:id", {
            templateUrl: "EditCompte.html",
            controller: "AccountBDController"
=======
        .when("/ModifyAccount/:id", {
            templateUrl: "EditCompte.html",
            controller: "AccountBDController"
>>>>>>> 5597514c2bf9093f555916b1ed1b3425ad0b830d
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
<<<<<<< HEAD
        .when("/CategoriesList", {
            templateUrl: "ListeCategories.html",
            controller: "CategorieBDController"
        })
        .when("/NewCategorie", {
            templateUrl: "AjouterCategorie.html",
            controller: "CategorieBDController"
=======
        .when("/CategoriesList", {
            templateUrl: "ListeCategories.html",
            controller: "CategorieBDController"
        })
        .when("/NewCategorie", {
            templateUrl: "AjouterCategorie.html",
            controller: "CategorieBDController"
>>>>>>> 5597514c2bf9093f555916b1ed1b3425ad0b830d
        })


        
<<<<<<< HEAD
    .otherwise({ redirectTo: "/Home" })
=======
    .otherwise({ redirectTo: "/Home" })
>>>>>>> 5597514c2bf9093f555916b1ed1b3425ad0b830d
});
