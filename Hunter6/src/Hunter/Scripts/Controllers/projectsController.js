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

    function editProjectInstanceController($scope, $uibModalInstance, $http, alertService, id) {
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
                    alertService.addDangerAlert("Editor error");
                });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

    function projectsController($scope, $uibModal, $http, alertService, Projects, FileUploader) {
        $scope.Projects = Projects.query();
        $scope.uploader = new FileUploader();

        $scope.add = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/modal/EditProjectModal.html',
                controller: 'AddProjectInstanceCtrl'
            });

            modalInstance.result.then(function () {
                $scope.Projects = Projects.query();

                alertService.addSuccessAlert('Project was successfully added');
            });
        };

        $scope.edit = function (_id) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/modal/EditProjectModal.html',
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
                    alertService.addSuccessAlert('Project was successfully deleted');
                    $scope.Projects = Projects.query();
                })
                .error(function () {
                    alertService.addDangerAlert('Error was occured during the project removal');
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
        .module('app')
        .controller('projectsController', projectsController)
        .controller('AddProjectInstanceCtrl', addProjectInstanceController)
        .controller('EditProjectInstanceCtrl', editProjectInstanceController);

    projectsController.$inject = ['$scope', '$uibModal', '$http', 'alertService', 'Projects', 'FileUploader'];
    addProjectInstanceController.$inject = ['$scope', '$uibModalInstance', '$http'];
    editProjectInstanceController.$inject = ['$scope', '$uibModalInstance', '$http', 'alertService', 'id'];


})();