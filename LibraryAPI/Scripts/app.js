// Starting the angular app.

angular.module("LibraryApp", [])
    .controller("mainAppController", ["$scope", "$http", ($scope, $http) => {
        // The bookList value to be manipulated.
        let bookList = [];

        // The book Information to be returned.
        let bookInfo = {};

        let currentBookID = "";
        let currentCheckoutProcess = "";

        $scope.checkoutProcess = false;
        $scope.searchIsActive = false;

        // The bookList value to be returned.
        $scope.bookList;

        // An object that contains all author information. To interact with the forms.
        $scope.bookInfo = {
            Title: "",
            Author: "",
            Genre: "",
        };

        $scope.greeting = "Hello World!";

        $scope.userEmail = "";

        $scope.searchBooks = () => {
            // Grab the scope information.
            bookInfo = $scope.bookInfo;
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

        $scope.checkOutIn = (id, type) => {
            console.log(id, type);
            currentBookID = id;
            currentCheckout = type;
            $scope.checkoutProcess = true;
            // if checking in, just run the function.
            if (currentCheckout == 'checkin') {
                putBook(id, currentCheckout);
            }
        }

        $scope.checkOut = () => {
            let userEmail = $scope.userEmail;
            $scope.userEmail = "";
            putBook(currentBookID, currentCheckout, userEmail);

        }

        // does a simple GET request to get all books.
        const getBooks = (url) => {
            return $http({
                method: "GET",
                url: url,
            }).then(response => {
                $scope.searchIsActive = true;
                bookList = response.data;
                console.log(bookList);
                $scope.bookList = bookList;
            });
        }

        // does a simple PUT request to check out a book.
        const putBook = (id, type, email) => {
            console.log(id, type);
            return $http({
                method: "PUT",
                url: `/api/books/${type}/${id}`,
                data: {
                    Email: email
                }
            }).then(response => {
                console.log(response);
                $scope.checkoutProcess = false;
                // Now update all books.
                getBooks('/api/books');
            });
        }


    }]);