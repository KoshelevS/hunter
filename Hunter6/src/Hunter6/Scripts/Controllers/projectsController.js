(function () {
    'use strict';

    angular
        .module('projectsApp')
        .controller('projectsController', projectsController)
        .controller('ProjectInstanceCtrl', projectInstanceController);

    projectsController.$inject = ['$scope', '$uibModal', '$http', 'Projects'];
    projectInstanceController.$inject = ['$scope', '$uibModalInstance', '$http', 'id'];

    function projectsController($scope, $uibModal, $http, Projects) {
        $scope.Projects = Projects.query();

        $scope.edit = function (_id) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/static/EditProjectModal.html',
                controller: 'ProjectInstanceCtrl',
                resolve: {
                    id: function () { return _id; }
                }
            });
        };

        $scope.delete = function (_id) {
            $http.delete('/api/project/' + _id).success(function () {
                alert("The project was deleted successfully.");
            });
        };

        $scope.gridOptions = {
            enableFiltering: true,
            columnDefs: [
              { name: 'ID', field: 'Id' },
              { name: 'Name', field: 'Name' },
              { name: 'Vacancies', field: 'Vacancies[0].Name' },
              { name: 'Actions', enableFiltering: false, cellTemplate: '<div><button ng-click="grid.appScope.edit(row.entity.Id)">Edit</button><button ng-click="grid.appScope.delete(row.entity.Id)">Delete</button></div>', sortable: false }
            ]
        };                        

        $scope.gridOptions.data = "Projects";
    }

    function projectInstanceController($scope, $uibModalInstance, $http, id) {
        $http.get('/api/project/' + id).success(function (data) {
            $scope.project = data;
        });

        $scope.ok = function () {
            $http.put('/api/project/' + id, $scope.project ).success(function () {
                alert("The project was updated successfully.");

                $uibModalInstance.close();
            });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    };
})();