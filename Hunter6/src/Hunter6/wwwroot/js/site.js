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

    var projectsService = angular.module('projectsService', ['ngResource']);
    projectsService.factory('Projects', ['$resource',
        function ($resource) {
            return $resource('/api/project', {}, {
                query: { method: 'GET', params: {}, isArray: true }
            });
        }
    ]);
})();
(function () {
    'use strict';

    function projectInstanceController($scope, $uibModalInstance, $http, id) {
        $http.get('/api/project/' + id)
            .success(function (data) {
                $scope.project = data;
            });

        $scope.ok = function() {
            $http.put('/api/project/' + id, $scope.project)
                .success(function() {
                    $uibModalInstance.close();
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
                { name: 'ID', field: 'Id' },
                { name: 'Name', field: 'Name' },
                { name: 'Vacancies', field: 'Vacancies[0].Name' },
                {
                    name: 'Actions',
                    enableFiltering: false,
                    cellTemplate:
                        '<div><button class="btn btn-warning" ng-click="grid.appScope.edit(row.entity.Id)"><span class="glyphicon glyphicon-pencil"></span> Edit</button>' +
                            '<button class="btn btn-danger" ng-click="grid.appScope.delete(row.entity.Id)"><span class="glyphicon glyphicon-remove"></span> Delete</button></div>',
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