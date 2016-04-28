(function () {

    function alertsController($scope, alertService) {
        alertService.setScope($scope);

        $scope.alertDismissTimeout = HunterSettings.AlertDismissTimeoutInMilliseconds;

        $scope.closeAlert = function (index) {
            alertService.removeAlert(index);
        };
    }

    angular
        .module('app')
        .controller('alertsController', alertsController);

    alertsController.$inject = ['$scope', 'alertService'];
})();