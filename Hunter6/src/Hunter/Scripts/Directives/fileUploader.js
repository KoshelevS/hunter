(function () {
    angular
    .module('app')
    .directive("fileUploader", function () {
            return {
                restrict: "A",
                templateUrl: "/html/controls/FileUploader.html"
            };
        });
})();