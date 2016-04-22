(function () {

    function addCandidateInstanceController($scope, $uibModalInstance, $http) {
        $scope.title = 'Add Candidate';

        $scope.ok = function () {
            $http.post('/api/applicant/', $scope.candidate)
                .success(function () {
                    $uibModalInstance.close();
                });
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

    function editCandidateInstanceController($scope, $uibModalInstance, $http, id) {
        $scope.title = 'Edit Candidate';

        $http.get('/api/applicant/' + id)
            .success(function (data) {
                $scope.candidate = data;
            });

        $scope.ok = function () {
            $http.put('/api/applicant/' + id, $scope.candidate)
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

    function candidateController($scope, $uibModal, $http, Candidate) {
        $scope.Candidate = Candidate.query();

        $scope.add = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/AddCandidateModal.html',
                controller: 'AddCandidateInstanceCtrl'
            });

            modalInstance.result.then(function () {
                $scope.Candidate = Candidate.query();

                HunterAlerts.addSuccessAlert('Candidate was successfully added');
            });
        };

        $scope.edit = function (_id) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/html/AddCandidateModal.html',
                controller: 'EditCandidateInstanceCtrl',
                resolve: {
                    id: function () { return _id; }
                }
            });

            modalInstance.result.then(function () {
                $scope.candidate = Candidate.query();
            });
        };

        $scope.delete = function (_id) {
            $http
                .delete('/api/applicant/' + _id)
                .success(function () {
                    HunterAlerts.addSuccessAlert('Candidate was successfully deleted');
                    $scope.Candidate = Candidate.query();
                })
                .error(function () {
                    HunterAlerts.addDangerAlert('Error was occured during the removal');
                    $scope.Candidate = Candidate.query();
                });
        };

        $scope.gridOptions = {
            enableFiltering: true,
            columnDefs: [
                { name: 'ID', field: 'ID' },
                { name: 'Name', field: 'Name' },
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

        $scope.gridOptions.data = "Candidate";
    }

    angular
        .module('app')
        .controller('candidateController', candidateController)
        .controller('AddCandidateInstanceCtrl', addCandidateInstanceController)
        .controller('EditProjectInstanceCtrl', editCandidateInstanceController);

    candidateController.$inject = ['$scope', '$uibModal', '$http', 'Candidate'];
    addCandidateInstanceController.$inject = ['$scope', '$uibModalInstance', '$http'];
    editCandidateInstanceController.$inject = ['$scope', '$uibModalInstance', '$http', 'id'];


})();