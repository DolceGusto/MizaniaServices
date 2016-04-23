(function () {
    var usersService = function ($http, $q, $log) {


        var Users = function () {
            return $http.get("http://localhost:26309/api/User/getAll/")
                        .then(function (serviceResp) {
                            return serviceResp.data;
                        });
        };

        var SearchUsers = function (UserName) {
            return $http.get("http://localhost:26309/api/User/getUserByNom/" + UserName)
            .then(function (serviceResp) {
                return serviceResp.data;
            });
        };



        var InsertUser = function (user) {
            return $http.post("http://localhost:26309/api/User/addUser", user)
            .then(function () {
                $log.info("Insert Successful");
                return;
            });
        };

        var singleUser = function (id) {
            return $http.get("http://localhost:26309/api/User/getUserById/" + id)
                        .then(function (serviceResp) {
                            return serviceResp.data;
                        });
        };

        var ModifyUser = function (user) {
            return $http.put("http://localhost:26309/api/User/updateUser/" + user.id, user)
            .then(function (result) {
                $log.info("Update Successful");
                return;
            });
        };

        var UserAccounts = function (user) {
            return $http.get("http://localhost:26309/api/User/getUserAccounts/" + user.id, user)
            .then(function (serviceResp) {
                return serviceResp.data;
            });
        };

        var deleteUser = function (user) {
            return $http.delete("http://localhost:26309/api/User/deleteUser/" + user.id)
            .then(function (result) {
                $log.info("Delete Successful");
                return;
            });
        };

        return {
            Users: Users,
            singleUser: singleUser,
            SearchUsers: SearchUsers,
            InsertUser: InsertUser,
            ModifyUser: ModifyUser,
            deleteUser: deleteUser,
            UserAccounts: UserAccounts
        };
    };
    var module = angular.module("MizaniaProjectModule");
    module.factory("usersService", ["$http", "$q", "$log", usersService]);
}());