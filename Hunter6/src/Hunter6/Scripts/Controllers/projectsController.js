(function () {
    'use strict';

    angular
        .module('projectsApp')
        .controller('projectsController', projectsController);

    projectsController.$inject = ['$scope', 'Projects'];

    function projectsController($scope, Projects) {
        $scope.Projects = Projects.query();
    }
})();
