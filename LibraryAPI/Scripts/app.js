// Starting the angular app.

angular.module("LibraryApp", [])
    .controller("mainAppController", ["$scope", "$http", ($scope, $http) => {

        $scope.greeting = "Hello World!";

    }]);