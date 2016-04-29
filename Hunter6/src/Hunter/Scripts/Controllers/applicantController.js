(function () {

    function addApplicantInstanceController($scope, $uibModalInstance, applicantService) {
        $scope.title = 'Add Applicant';

        $scope.ok = function () {
            applicantService.create({ }, $scope.applicant,
                function (data) {
                    $uibModalInstance.close();
                }
            );
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

    function editApplicantInstanceController($scope, $uibModalInstance, applicantService, alertService, id) {
        $scope.title = 'Edit Applicant';

        applicantService.get({ id: id },
            function (data) {
                $scope.applicant = data;
            }
        );

        $scope.ok = function () {
            applicantService.update({ id: id }, $scope.applicant,
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

    function applicantController($scope, $uibModal, alertService, applicantService) {
        $scope.Applicant = applicantService.query();

        $scope.add = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/modal/AddApplicantModal.html',
                controller: 'AddApplicantInstanceCtrl'
            });

            modalInstance.result.then(function () {
                $scope.Applicant = applicantService.query();

                alertService.addSuccessAlert('Applicant was successfully added');
            });
        };

        $scope.edit = function (_id) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/modal/AddApplicantModal.html',
                controller: 'EditApplicantInstanceCtrl',
                resolve: {
                    id: function () { return _id; }
                }
            });

            modalInstance.result.then(function () {
                $scope.applicant = applicantService.query();
            });
        };

        //        $scope.delete = applicantService.remove(_id)
        //            .success(function() {
        //                alertService.addSuccessAlert('Applicant was successfully deleted');
        //                $scope.Applicant = applicantService.query();
        //            })
        //            .error(function() {
        //                alertService.addDangerAlert('Error was occured during the removal');
        //                $scope.Applicant = applicantService.query();
        //            });

        $scope.delete = function (_id) {
            applicantService.remove({ id: _id },
                function (data) {
                    alertService.addSuccessAlert('Applicant was successfully deleted');
                    $scope.Applicant = applicantService.query();
                },
                function (error) {
                    alertService.addDangerAlert('Error was occured during the removal');
                    $scope.Applicant = applicantService.query();
                }
            );
        };

        $scope.gridOptions = {
            enableFiltering: true,
            enableRowSelection: true,
            multiSelect: false,
            enableColumnResize: true,
            columnDefs: [
                { name: 'ID', field: 'ID' },
                { name: 'Name', field: 'Name' },
                { name: 'Phone', field: 'Phone' },
                { field: 'Birthday', displayName: 'Birthday', type: 'date', cellFilter: 'date:\'yyyy-MM-dd\'' },
                {
                    name: 'Actions',
                    enableFiltering: false,
                    cellTemplate:
                        '<div><button class="btn btn-warning" ng-click="grid.appScope.edit(row.entity.ID)"><span class="glyphicon glyphicon-pencil"></span> Edit</button>' +
                            '<button class="btn btn-danger" ng-click="grid.appScope.delete(row.entity.ID)"><span class="glyphicon glyphicon-remove"></span> Delete</button></div>',
                    sortable: false
                }
            ],
            data: 'Applicant'
            //data:applicantService.query()
            //data:$scope.Applicant
        };

        //$scope.gridOptions.data = 'Applicant';
        //$scope.gridOptions.data = applicantService.query();
        //$scope.gridOptions.data = $scope.Applicant;
    }

    angular
        .module('app')
        .controller('applicantController', applicantController)
        .controller('AddApplicantInstanceCtrl', addApplicantInstanceController)
        .controller('EditApplicantInstanceCtrl', editApplicantInstanceController);

    applicantController.$inject = ['$scope', '$uibModal', 'alertService', 'applicantService'];
    addApplicantInstanceController.$inject = ['$scope', '$uibModalInstance', 'applicantService'];
    editApplicantInstanceController.$inject = ['$scope', '$uibModalInstance', 'applicantService', 'alertService', 'id'];


})();