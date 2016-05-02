(function () {

    function addProjectInstanceController($scope, $uibModalInstance, Projects) {
        $scope.title = 'Add Project';

        $scope.ok = function () {
            Projects.create({}, $scope.project,
                function (data) {
                    $uibModalInstance.close();
                }
            );
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

    function editProjectInstanceController($scope, $uibModalInstance, Projects, alertService, id) {
        $scope.title = 'Edit Project';

        Projects.get({ id: id },
            function (data) {
                $scope.project = data;
            }
        );

        $scope.ok = function () {
            Projects.update({ id: id }, $scope.project,
                function (data) {
                    $uibModalInstance.close();
                },
                function (error) {
                    $uibModalInstance.close();
                    alertService.addDangerAlert("Editor error");
                }
            );
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

    function projectsController($scope, $uibModal, alertService, Projects, FileUploader) {
        $scope.Projects = Projects.query();
        $scope.uploader = new FileUploader({
            url: 'api/FileUpload/files'
        });

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
            Projects.remove({ id: _id },
                function (data) {
                    alertService.addSuccessAlert('Project was successfully deleted');
                    $scope.Projects = Projects.query();
                },
                function (error) {
                    alertService.addDangerAlert('Error was occured during the project removal');
                    $scope.Projects = Projects.query();
                }
            );
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

    projectsController.$inject = ['$scope', '$uibModal', 'alertService', 'Projects', 'FileUploader'];
    addProjectInstanceController.$inject = ['$scope', '$uibModalInstance', 'Projects'];
    editProjectInstanceController.$inject = ['$scope', '$uibModalInstance', 'Projects', 'alertService', 'id'];


})();