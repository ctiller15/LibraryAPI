// Starting the angular app.

angular.module("LibraryApp", [])
    .controller("mainAppController", ["$scope", "$http", ($scope, $http) => {
        // The bookList value to be manipulated.
        let bookList = [];

        // The book Information to be returned.
        let bookInfo = {};

        // The bookList value to be returned.
        $scope.bookList;

        // An object that contains all author information. To interact with the forms.
        $scope.bookInfo = {
            Title: "",
            Author: "",
            Genre: "",
        };

        $scope.greeting = "Hello World!";

        $scope.searchBooks = () => {
            // Grab the scope information.
            bookInfo = $scope.bookInfo;
            console.log(bookInfo);
            let title = bookInfo.Title;
            let author = bookInfo.Author;
            let genre = bookInfo.Genre;

            // Clear the scope to give some information back to the user.
            $scope.bookInfo.Title = $scope.bookInfo.Author = $scope.bookInfo.Genre = "";

            // Title, Author, and Genre used as optional search terms.

            // Create URL.
            let searchURL = `/api/books`
            if (title || author || genre) {
                searchURL += `/?`;
            }
            if (title) {
                searchURL += `BookTitle=${title}&`;
            }
            if (author) {
                searchURL += `BookAuthor=${author}&`;
            }
            if (genre) {
                searchURL += `BookGenre=${genre}&`;
            }

            console.log(searchURL);

            // To be taken from a form.
            getBooks(searchURL);
        }

        // does a simple GET request to get all books.
        const getBooks = (url) => {
            return $http({
                method: "GET",
                url: url,
            }).then(response => {
                bookList = response.data;
                console.log(bookList);
                $scope.bookList = bookList;
            });
        }



    }]);