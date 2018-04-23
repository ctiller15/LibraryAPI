// Starting the angular app.

angular.module("LibraryApp", [])
    .controller("mainAppController", ["$scope", "$http", ($scope, $http) => {

        $scope.greeting = "Hello World!";

        $scope.getBooks = () => {
            $http({
                method: "GET",
                url: "/api/books"
            }).then(response => {
                console.log(response.data);
            });
        }

    }]);