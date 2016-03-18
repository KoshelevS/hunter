(function () {
    'use strict';

    angular.module('projectsApp', [
        'projectsService',
        'ui.grid',
        'ui.bootstrap'
    ]);
})();
(function () {
    'use strict';

    function projectInstanceController($scope, $uibModalInstance, $http, id) {
        $http.get('/api/project/' + id).then(
//        $http.get('/api/ProjectsNew/' + id).then(
            function (data) {
                $scope.project = data.data;
            },
            function (data) {
                alert(data.statusText);
                $uibModalInstance.close();
            });

        $scope.ok = function () {
            $http.put('/api/project/' + id, $scope.project)
//            $http.put('/api/ProjectsNew/' + id, $scope.project)
                .success(function () {
                    $uibModalInstance.close();
                })
                .error(function (data) {
                    alert(data.Message);
                });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    };

    function projectsController($scope, $uibModal, $http, Projects) {
        $scope.Projects = Projects.query();

        $scope.edit = function (_id) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/EditProjectModal.html',
                controller: 'ProjectInstanceCtrl',
                resolve: {
                    id: function () { return _id; }
                }
            });
            modalInstance.result.then(function () {
                $scope.Projects = Projects.query();
            });

        };

        $scope.delete = function (_id) {
            $http.delete('/api/project/' + _id).success(function () {
                alert("The project was deleted successfully.");
                $scope.Projects = Projects.query();
            });
        };

        $scope.gridOptions = {
            enableFiltering: true,
            columnDefs: [
                { name: '--ID--', field: 'ID' },
                { name: 'Name', field: 'Name' },
                { name: 'FirstVacancy', field: 'FirstVacancy' },
                {
                    name: 'Actions',
                    enableFiltering: false,
                    cellTemplate:
                        '<div><button ng-click="grid.appScope.edit(row.entity.ID)">Edit</button>' +
                            '<button ng-click="grid.appScope.delete(row.entity.ID)">Delete</button></div>',
                    sortable: false
                }
            ]
        };

        $scope.gridOptions.data = "Projects";
    }

    angular
        .module('projectsApp')
        .controller('projectsController', projectsController)
        .controller('ProjectInstanceCtrl', projectInstanceController);

    projectsController.$inject = ['$scope', '$uibModal', '$http', 'Projects'];
    projectInstanceController.$inject = ['$scope', '$uibModalInstance', '$http', 'id'];


})();
(function () {
    'use strict';

    var projectsService = angular.module('projectsService', ['ngResource']);
    projectsService.factory('Projects', ['$resource',
        function ($resource) {
            return $resource('/api/project', {}, {
//            return $resource('/api/ProjectsNew', {}, {
                query: { method: 'GET', params: {}, isArray: true }
            });
        }
    ]);
})();