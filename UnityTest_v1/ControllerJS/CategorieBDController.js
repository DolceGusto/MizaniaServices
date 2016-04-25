(function () {
    var CategorieBDController = function ($scope, categoriesService, $log, $routeParams) {
        var Categories = function (data) {
            $scope.Categories = data;
        };

        var singleCategorie = function (data) {
            $scope.existingCategorie = data;
            $log.info(data);
        };


        $scope.init = function () {
            categoriesService.singleCategorie($routeParams.id)
                .then(singleCategorie, errorDetails);
        };

        var categorie = {
            id: null,
            idPorteFeuille: 1,
            desination: null,
            descript : null
        };
        $scope.categorie = categorie;

        categoriesService.Categories().then(Categories, errorDetails);

        var errorDetails = function (serviceResp) {
            $scope.Error = "Something went wrong ??";
        };

        $scope.InsertCategorie = function (categorie) {
            categoriesService.InsertCategorie(categorie).then(categoriesService.Categories().then(Categories, errorDetails));

        };

        $scope.ModifyCategorie = function (existingCategorie) {
            $log.info(existingCatgorie);
            categoriesService.ModifyCategorie(existingCategorie).then(categoriesService.Categories().then(Categories, errorDetails));
        };

        $scope.deleteCategorie = function (categorie) {
            $log.info(categorie);
            if (confirm("Etes-vous sûr de vouloir supprimer cette catégorie ?")) {
                categoriesService.deleteCategorie(categorie)
                    .then(Categories, errorDetails);
            }
        };


      
    };
    app.controller("CategorieBDController", ["$scope", "categoriesService", "$log", "$routeParams", CategorieBDController]);
}());