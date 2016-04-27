(function () {

    function addApplicantInstanceController($scope, $uibModalInstance, $http) {
        $scope.title = 'Add Applicant';

        $scope.ok = function () {
            $http.post('/api/applicant/', $scope.applicant)
                .success(function () {
                    $uibModalInstance.close();
                });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

    function editApplicantInstanceController($scope, $uibModalInstance, $http, id) {
        $scope.title = 'Edit Applicant';

        $http.get('/api/applicant/' + id)
            .success(function (data) {
                $scope.applicant = data;
            });

        $scope.ok = function () {
            $http.put('/api/applicant/' + id, $scope.applicant)
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

    function applicantController($scope, $uibModal, $http, applicantService) {
        $scope.Applicant = applicantService.query();

        $scope.add = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/modal/AddApplicantModal.html',
                controller: 'AddApplicantInstanceCtrl'
            });

            modalInstance.result.then(function () {
                $scope.Applicant = applicantService.query();

                HunterAlerts.addSuccessAlert('Applicant was successfully added');
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
        //                HunterAlerts.addSuccessAlert('Applicant was successfully deleted');
        //                $scope.Applicant = applicantService.query();
        //            })
        //            .error(function() {
        //                HunterAlerts.addDangerAlert('Error was occured during the removal');
        //                $scope.Applicant = applicantService.query();
        //            });
        $scope.delete = function (_id) {
            $http.delete('/api/applicant/' + _id)  //applicantService.remove(_id)
                .success(function () {
                    HunterAlerts.addSuccessAlert('Applicant was successfully deleted');
                    $scope.Applicant = applicantService.query();
                })
                .error(function () {
                    HunterAlerts.addDangerAlert('Error was occured during the removal');
                    $scope.Applicant = applicantService.query();
                });
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
            data:applicantService.query()
        };

        //$scope.gridOptions.data = 'Applicant';
        //$scope.gridOptions.data = applicantService.query();
        //$scope.gridOptions.data = $scope.Applicant;
    }

    angular
        .module('app')
        .controller('applicantController', applicantController)
        .controller('AddApplicantInstanceCtrl', addApplicantInstanceController)
        .controller('EditProjectInstanceCtrl', editApplicantInstanceController);

    applicantController.$inject = ['$scope', '$uibModal', '$http', 'applicantService'];
    addApplicantInstanceController.$inject = ['$scope', '$uibModalInstance', '$http'];
    editApplicantInstanceController.$inject = ['$scope', '$uibModalInstance', '$http', 'id'];


})();