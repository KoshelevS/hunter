(function () {

    function addProjectInstanceController($scope, $uibModalInstance, $http) {
        $scope.title = 'Add Project';

        $scope.ok = function () {
            $http.post('/api/project/', $scope.project)
                .success(function () {
                    $uibModalInstance.close();
                });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

    function editProjectInstanceController($scope, $uibModalInstance, $http, id) {
        $scope.title = 'Edit Project';

        $http.get('/api/project/' + id)
            .success(function (data) {
                $scope.project = data;
            });

        $scope.ok = function () {
            $http.put('/api/project/' + id, $scope.project)
                .success(function () {
                    $uibModalInstance.close();
                })
                .error(function () {
                    $uibModalInstance.close();
                    HunterAlerts.addDangerAlert("Editor error");
                });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

    function projectsController($scope, $uibModal, $http, Projects) {
        $scope.Projects = Projects.query();

        $scope.add = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/EditProjectModal.html',
                controller: 'AddProjectInstanceCtrl'
            });

            modalInstance.result.then(function () {
                $scope.Projects = Projects.query();

                HunterAlerts.addSuccessAlert('Project was successfully added');
            });
        };

        $scope.edit = function (_id) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/EditProjectModal.html',
                controller: 'EditProjectInstanceCtrl',
                resolve: {
                    id: function () { return _id; }
                }
            });

            modalInstance.result.then(function () {
                $scope.Projects = Projects.query();
            });
        };

        $scope.delete = function (_id) {
            $http
                .delete('/api/project/' + _id)
                .success(function () {
                    HunterAlerts.addSuccessAlert('Project was successfully deleted');
                    $scope.Projects = Projects.query();
                })
                .error(function () {
                    HunterAlerts.addDangerAlert('Error was occured during the project removal');
                    $scope.Projects = Projects.query();
                });
        };

        $scope.gridOptions = {
            enableFiltering: true,
            columnDefs: [
                { name: 'ID', field: 'ID' },
                { name: 'Name', field: 'Name' },
                { name: 'Vacancies', field: 'FirstVacancy' },
                {
                    name: 'Actions',
                    enableFiltering: false,
                    cellTemplate:
                        '<div><button class="btn btn-warning" ng-click="grid.appScope.edit(row.entity.ID)"><span class="glyphicon glyphicon-pencil"></span> Edit</button>' +
                            '<button class="btn btn-danger" ng-click="grid.appScope.delete(row.entity.ID)"><span class="glyphicon glyphicon-remove"></span> Delete</button></div>',
                    sortable: false
                }
            ]
        };

        $scope.gridOptions.data = "Projects";
    }

    angular
        .module('projectsApp')
        .controller('projectsController', projectsController)
        .controller('AddProjectInstanceCtrl', addProjectInstanceController)
        .controller('EditProjectInstanceCtrl', editProjectInstanceController);

    projectsController.$inject = ['$scope', '$uibModal', '$http', 'Projects'];
    addProjectInstanceController.$inject = ['$scope', '$uibModalInstance', '$http'];
    editProjectInstanceController.$inject = ['$scope', '$uibModalInstance', '$http', 'id'];


})();