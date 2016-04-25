(function () {
    var categoriesService = function ($http, $q, $log) {


        var Categories = function () {
            return $http.get("http://localhost:26309/api/Categorie/getAll/")
                        .then(function (serviceResp) {
                            return serviceResp.data;
                        });
        };

       


        var InsertCategorie = function (categorie) {
            return $http.post("http://localhost:26309/api/Categorie/addCategorie", categorie)
            .then(function () {
                $log.info("Insert Successful");
                return;
            });
        };

        var singleCategorie = function (id) {
            return $http.get("http://localhost:26309/api/Categorie/getCategorieById/" + id)
                        .then(function (serviceResp) {
                            return serviceResp.data;
                        });
        };

        var ModifyCategorie = function (categorie) {
            return $http.put("http://localhost:26309/api/Categorie/updateCategorie/" + categorie.id, categorie)
            .then(function (result) {
                $log.info("Update Successful");
                return;
            });
        };

      

        var deleteCategorie = function (categorie) {
            return $http.delete("http://localhost:26309/api/Categorie/deleteCategorie/" + categorie.id)
            .then(function (result) {
                $log.info("Delete Successful");
                return;
            });
        };

        return {
            Categories: Categories,
            singleCategorie: singleCategorie,
            InsertCategorie: InsertCategorie,
            ModifyCategorie: ModifyCategorie,
            deleteCategorie: deleteCategorie
        };
    };
    var module = angular.module("MizaniaProjectModule");
    module.factory("categoriesService", ["$http", "$q", "$log", categoriesService]);
}());