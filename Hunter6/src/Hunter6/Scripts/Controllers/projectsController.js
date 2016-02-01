(function () {
    'use strict';

    angular
        .module('projectsApp')
        .controller('projectsController', projectsController);

    projectsController.$inject = ['$scope', 'Projects'];

    function projectsController($scope, Projects) {
        $scope.Projects = Projects.query();

        $scope.gridOptions = {
            enableFiltering: true,
            columnDefs: [
              { name: 'ID', field: 'ID' },
              { name: 'Name', field: 'Name' },
              { name: 'Vacancies', field: 'Vacancies[0].Name' }
            ]
        };

        $scope.gridOptions.data = $scope.Projects;
    }
})();